using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReLux.DataAccess.Repository.IRepository;
using ReLux.Models;

namespace ReLuxWeb.Pages.Admin.RateConditions
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public RateCondition RateCondition { get; set; }

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            RateCondition = _unitOfWork.RateCondition.GetFirstOrDefault(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            var rateConditionFromDb = _unitOfWork.RateCondition.GetFirstOrDefault(u => u.Id == RateCondition.Id);
            if (rateConditionFromDb != null)
            {
                _unitOfWork.RateCondition.Remove(rateConditionFromDb);
                _unitOfWork.Save();
                TempData["error"] = "Rate Condition deleted successfully!";
                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}

