using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakersStore.Models;
using SneakersStore.DataAccess.Data;
using SneakersStore.DataAccess.Repository.IRepository;

namespace Sneakers_store_web.Pages.Admin.ShoeTypes
{
    [BindProperties]
    public class EditModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        public ShoeType ShoeType { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            ShoeType = _unitOfWork.ShoeType.GetFirstOrDefault(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {            
            if (ModelState.IsValid)
            {
                _unitOfWork.ShoeType.Update(ShoeType);
                _unitOfWork.Save();
                TempData["success"] = "Shoe Type updated successfully";
                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}
