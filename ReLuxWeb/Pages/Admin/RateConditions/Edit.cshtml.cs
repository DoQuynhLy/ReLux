using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReLux.DataAccess.Repository.IRepository;
using ReLux.Models;

namespace ReLuxWeb.Pages.Admin.RateConditions
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public RateCondition RateCondition { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            RateCondition = _unitOfWork.RateCondition.GetFirstOrDefault(x => x.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.RateCondition.Update(RateCondition);
                _unitOfWork.Save();
                TempData["success"] = "Rate Condition updated successfully!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
