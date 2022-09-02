using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReLux.DataAccess.Repository.IRepository;
using ReLux.Models;

namespace ReLuxWeb.Pages.Admin.RateConditions
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<RateCondition> RateConditions { get; set; }
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            RateConditions = _unitOfWork.RateCondition.GetAll();
        }
    }
}
