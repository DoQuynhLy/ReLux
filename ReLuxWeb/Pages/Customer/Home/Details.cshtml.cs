using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReLux.DataAccess.Repository.IRepository;
using ReLux.Models;
using System.ComponentModel.DataAnnotations;

namespace ReLuxWeb.Pages.Customer.Home
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Product Product { get; set; }

        public void OnGet(int id)
        {
            Product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id,includeProperties:"Category,RateCondition");
        }

    }
}
