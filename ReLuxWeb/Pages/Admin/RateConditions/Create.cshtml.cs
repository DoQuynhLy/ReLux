using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReLux.DataAccess.Repository.IRepository;
using ReLux.Models;

namespace ReLuxWeb.Pages.Admin.RateConditions
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public RateCondition RateCondition { get; set; }

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
                _unitOfWork.RateCondition.Add(RateCondition);
                _unitOfWork.Save();
                TempData["success"] = "Rate Condition created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
