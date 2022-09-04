using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReLux.DataAccess.Repository.IRepository;
using ReLux.Models;
using System.Security.Claims;

namespace ReLuxWeb.Pages.Customer.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        //This is the collection of shopping cart
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public double CartTotal { set; get; }

        private readonly IUnitOfWork _unitOfWork;
        public IndexModel (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartTotal = 0;
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties:"Product,Product.Category,Product.RateCondition" );

                foreach (var cartItem in ShoppingCartList)
                {
                    CartTotal += (cartItem.Product.Price * cartItem.Count);
                }
            }
        }

        public IActionResult OnPostRemove( int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            return RedirectToPage("/Customer/Cart/Index");
        }
    }
}
