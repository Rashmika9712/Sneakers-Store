using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakersStore.Models;
using SneakersStore.DataAccess.Data;
using SneakersStore.DataAccess.Repository.IRepository;

namespace Sneakers_store_web.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<Category> Categories { get; set; }      
        

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            Categories = _unitOfWork.Category.GetAll();
        }
        
    }
}
