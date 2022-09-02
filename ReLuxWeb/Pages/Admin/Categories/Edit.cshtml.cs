using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReLux.DataAccess.Repository.IRepository;
using ReLux.Models;

namespace ReLuxWeb.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public Category Category { get; set; }

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            Category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(Category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
