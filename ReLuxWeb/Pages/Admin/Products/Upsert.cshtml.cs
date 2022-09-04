using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReLux.DataAccess.Repository.IRepository;
using ReLux.Models;
using ReLux.Utility;
using static System.Net.Mime.MediaTypeNames;

namespace ReLuxWeb.Pages.Admin.Products
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public Product Product { get; set; }

        // Create empty lists of category and rate conditions to display them in a dropbox
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> RateConditionList { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            Product = new();
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                //Edit
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            }
            // Generate a list of Category and RateCondition and store both in the empty lists
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            RateConditionList = _unitOfWork.RateCondition.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Description,
                Value = i.Id.ToString()
            });
        }

        public async Task<IActionResult> OnPost()
        {

            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (Product.Id == 0)
            {
                //create
                string fileName_new = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"Images\Product");
                var extension = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                Product.Image = @"\Images\Product\" + fileName_new + extension;

                string isSold = Request.Form["isSold"].ToString();
                if (isSold == "1")
                {
                    Product.IsSold = true;
                }
                else
                {
                    Product.IsSold= false;
                }
                _unitOfWork.Product.Add(Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
            }
            else
            {
                //edit
                var objFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == Product.Id);
                
                if (files.Count > 0)
                {
                    string fileName_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\Product");
                    var extension = Path.GetExtension(files[0].FileName);

                    // Delete the old image
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    // New Upload
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    Product.Image = @"\Images\Product\" + fileName_new + extension;

                }
                else
                {
                    Product.Image = objFromDb.Image;
                }
                _unitOfWork.Product.Update(Product);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
            }

            return RedirectToPage("./Index");

        }
    }
}
