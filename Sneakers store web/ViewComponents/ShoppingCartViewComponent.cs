using Microsoft.AspNetCore.Mvc;
using SneakersStore.DataAccess.Repository.IRepository;
using SneakersStore.Utility;
using System.Security.Claims;

namespace Sneakers_store_web.ViewComponents
{
    public class ShoppingCartViewComponent:ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim  = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int count = 0;
            if(claim != null)
            {
                //user is logged in
                if(HttpContext.Session.GetInt32(SD.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }
                else
                {
                    count = _unitOfWork.ShoppingCart.GetAll(u=>u.ApplicationUserId==claim.Value).ToList().Count;
                    HttpContext.Session.SetInt32(SD.SessionCart, count);
                    return View(count);
                    HttpContext.Session.Clear();
                }
            }
            else
            {
                HttpContext.Session.Clear();
                //user has not logged in
                return View(count);
            }
        }
    }
}
