using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakersStore.Models;
using SneakersStore.DataAccess.Data;
using SneakersStore.DataAccess.Repository.IRepository;

namespace Sneakers_store_web.Pages.Admin.ShoeTypes
{
    [BindProperties]
    public class DeleteModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        public ShoeType ShoeType { get; set; }

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            ShoeType = _unitOfWork.ShoeType.GetFirstOrDefault(u=>u.Id==id);
        }

        public async Task<IActionResult> OnPost()
        {
            var shoeTypeFromDb = _unitOfWork.ShoeType.GetFirstOrDefault(u=>u.Id==ShoeType.Id);
            if(shoeTypeFromDb != null)
            {
                _unitOfWork.ShoeType.Remove(shoeTypeFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Shoe Type deleted successfully";
                return RedirectToPage("Index");
            }
            return Page();
            
        }
    }
}
