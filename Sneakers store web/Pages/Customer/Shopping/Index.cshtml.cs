using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakersStore.DataAccess.Repository.IRepository;
using SneakersStore.Models;

namespace Sneakers_store_web.Pages.Customer.Shopping
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ShoeItem> ShoeItemList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }

        public void OnGet()
        {
            ShoeItemList = _unitOfWork.ShoeItem.GetAll(includeProperties:"Category,ShoeType");
            CategoryList = _unitOfWork.Category.GetAll(orderby:u=>u.OrderBy(c=>c.DisplayOrder));
        }
    }
}
