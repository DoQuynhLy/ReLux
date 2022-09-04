using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReLux.DataAccess.Repository.IRepository;
using ReLux.Models;

namespace ReLuxWeb.Pages.Customer.Home
{
    [Authorize]
    public class IndexModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Product>  ProductList { get; set; }

        public IEnumerable<Category> CategoryList { get; set; }
        public void OnGet()
        {
            ProductList = _unitOfWork.Product.GetAll(includeProperties:"Category,RateCondition").Where(s => s.IsSold == false);
            CategoryList = _unitOfWork.Category.GetAll(orderby: u =>u.OrderBy(c=>c.DisplayOrder));
        }
    }
}
