using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakersStore.Models;
using SneakersStore.DataAccess.Data;
using SneakersStore.DataAccess.Repository;
using SneakersStore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace Sneakers_store_web.Pages.Admin.ShoeItems
{
    [BindProperties]
    public class UpsertModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ShoeItem ShoeItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ShoeTypeList { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            ShoeItem = new();
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                //Edit
                ShoeItem = _unitOfWork.ShoeItem.GetFirstOrDefault(u => u.Id == id);
            }            
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ShoeTypeList = _unitOfWork.ShoeType.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public async Task<IActionResult> OnPost()
        {
            string webRoothPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (ShoeItem.Id == 0)
            {
                //create
                string fileName_new = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRoothPath, @"images\menuItems");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                ShoeItem.Image = @"\images\menuItems\" + fileName_new + extension;
                _unitOfWork.ShoeItem.Add(ShoeItem);
                _unitOfWork.Save();
            }
            else
            {
                //edit
                var objFromDb = _unitOfWork.ShoeItem.GetFirstOrDefault(u => u.Id == ShoeItem.Id);
                if (files.Count > 0)
                {
                    string fileName_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRoothPath, @"images\menuItems");
                    var extension = Path.GetExtension(files[0].FileName);

                    //delete the old image 
                    var oldImagePath = Path.Combine(webRoothPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    //new upload
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    ShoeItem.Image = @"\images\menuItems\" + fileName_new + extension;
                }
                else
                {
                    ShoeItem.Image = objFromDb.Image;
                }

                _unitOfWork.ShoeItem.Update(ShoeItem);
                _unitOfWork.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
