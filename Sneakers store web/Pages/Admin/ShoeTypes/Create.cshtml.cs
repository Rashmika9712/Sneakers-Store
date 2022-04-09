using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakersStore.Models;
using SneakersStore.DataAccess.Data;
using SneakersStore.DataAccess.Repository;
using SneakersStore.DataAccess.Repository.IRepository;

namespace Sneakers_store_web.Pages.Admin.ShoeTypes
{
    [BindProperties]
    public class CreateModel : PageModel    
    {        

        private readonly IUnitOfWork _unitOfWork;
        public ShoeType ShoeType { get; set; }

        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {            
            if (ModelState.IsValid)
            {
                _unitOfWork.ShoeType.Add(ShoeType);
                _unitOfWork.Save();
                TempData["success"]="Shoe Type created successfully";
                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}
