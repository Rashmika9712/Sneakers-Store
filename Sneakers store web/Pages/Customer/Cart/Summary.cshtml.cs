using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SneakersStore.DataAccess.Repository.IRepository;
using SneakersStore.Models;
using SneakersStore.Utility;
using Stripe.Checkout;
using System.Security.Claims;

namespace Sneakers_store_web.Pages.Customer.Cart
{
    [Authorize]
    [BindProperties]
    public class SummaryModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }

        private readonly IUnitOfWork _unitOfWork;
        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderHeader = new OrderHeader();
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                   includeProperties: "ShoeItem,ShoeItem.ShoeType,ShoeItem.Category");

                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.ShoeItem.Price * cartItem.Count);
                }
                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
                OrderHeader.PickUpName = applicationUser.FirstName + " " + applicationUser.LastName;
                OrderHeader.PhoneNumber = applicationUser.PhoneNumber;

            }
        }

        public IActionResult OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                   includeProperties: "ShoeItem,ShoeItem.ShoeType,ShoeItem.Category");

                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.ShoeItem.Price * cartItem.Count);
                }
                OrderHeader.Status = SD.StatusPending;
                OrderHeader.OrderDate = System.DateTime.Now;
                OrderHeader.UserId = claim.Value;
                OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() +
                    " " + OrderHeader.PickUpTime.ToShortTimeString());

                _unitOfWork.OrderHeader.Add(OrderHeader);
                _unitOfWork.Save();

                foreach (var item in ShoppingCartList)
                {
                    OrderDetails orderDetails = new()
                    {
                        ShoeItemId = item.ShoeItemId,
                        OrderId = OrderHeader.Id,
                        Name = item.ShoeItem.Name,
                        Price = item.ShoeItem.Price,
                        Count = item.Count
                    };
                    _unitOfWork.OrderDetail.Add(orderDetails);
                }
                //_unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);
                _unitOfWork.Save();

                //Stripe  
                var domain = "https://localhost:44377/";
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>(),

                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },

                    Mode = "payment",
                    SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={OrderHeader.Id}",
                    CancelUrl = domain + "customer/cart/index",
                };

                //Add line items
                foreach (var item in ShoppingCartList)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.ShoeItem.Price * 100),
                            Currency = "lkr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.ShoeItem.Name,
                            },
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);
                Response.Headers.Add("Location", session.Url);

                OrderHeader.SessionId = session.Id;
                OrderHeader.PaymentIntentId = session.PaymentIntentId;
                _unitOfWork.Save();
                return new StatusCodeResult(303);
            }
            return Page();
        }
    }
}
