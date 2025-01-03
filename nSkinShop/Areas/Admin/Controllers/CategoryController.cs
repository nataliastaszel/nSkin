using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nSkinShop.DataAccess.Repository;
using nSkinShop.Models;
using nSkinShop.Utility;

namespace nSkinShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var categoryList = _unitOfWork.Category.GetAll().ToList();
            return View(categoryList);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (IsInvalidCategory(category))
            {
                return View(category);
            }

            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            var category = GetCategoryById(id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (IsInvalidCategory(category))
            {
                return View(category);
            }

            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            var category = GetCategoryById(id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var category = GetCategoryById(id);
            if (category == null) return NotFound();

            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        
        private Category? GetCategoryById(int? id)
        {
            if (id == null || id == 0) return null;
            return _unitOfWork.Category.Get(c => c.Id == id);
        }

        private bool IsInvalidCategory(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order cannot exactly match the Name");
                return true;
            }
            return !ModelState.IsValid;
        }
    }
}