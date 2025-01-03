using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using nSkinShop.DataAccess.Repository;
using nSkinShop.Models;
using nSkinShop.Models.ViewModels;
using nSkinShop.Utility;
using System.IO;

namespace nSkinShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return View(productList);
    }

    public IActionResult Upsert(int? id)
    {
        var productVM = CreateProductViewModel(id);
        return View(productVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            productVM.CategoryList = GetCategorySelectList();
            return View(productVM);
        }

        if (file != null)
        {
            productVM.Product.ImageUrl = ProcessUploadedFile(file, productVM.Product.ImageUrl);
        }

        if (productVM.Product.Id == 0)
        {
            _unitOfWork.Product.Add(productVM.Product);
            TempData["success"] = "Product created successfully";
        }
        else
        {
            _unitOfWork.Product.Update(productVM.Product);
            TempData["success"] = "Product updated successfully";
        }

        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int? id)
    {
        var product = GetProductById(id);
        if (product == null) return NotFound();

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int? id)
    {
        var product = GetProductById(id);
        if (product == null) return NotFound();

        DeleteFileIfExists(product.ImageUrl);
        _unitOfWork.Product.Remove(product);
        _unitOfWork.Save();
        TempData["success"] = "Product deleted successfully";

        return RedirectToAction(nameof(Index));
    }
    
    private ProductVM CreateProductViewModel(int? id)
    {
        var product = id == null || id == 0 
            ? new Product() 
            : _unitOfWork.Product.Get(p => p.Id == id) ?? new Product();

        return new ProductVM
        {
            Product = product,
            CategoryList = GetCategorySelectList()
        };
    }

    private IEnumerable<SelectListItem> GetCategorySelectList()
    {
        return _unitOfWork.Category.GetAll().Select(category => new SelectListItem
        {
            Text = category.Name,
            Value = category.Id.ToString()
        });
    }

    private Product? GetProductById(int? id)
    {
        if (id == null || id == 0) return null;
        return _unitOfWork.Product.Get(p => p.Id == id);
    }

    private string? ProcessUploadedFile(IFormFile file, string? existingImageUrl)
    {
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        string productPath = Path.Combine(wwwRootPath, "images", "product");

        if (!Directory.Exists(productPath))
        {
            Directory.CreateDirectory(productPath);
        }

        DeleteFileIfExists(existingImageUrl);

        string filePath = Path.Combine(productPath, fileName);
        using var fileStream = new FileStream(filePath, FileMode.Create);
        file.CopyTo(fileStream);

        return Path.Combine("images", "product", fileName).Replace(Path.DirectorySeparatorChar, '/');
    }

    private void DeleteFileIfExists(string? filePath)
    {
        if (string.IsNullOrEmpty(filePath)) return;

        string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart(Path.DirectorySeparatorChar));
        if (System.IO.File.Exists(fullPath))
        {
            System.IO.File.Delete(fullPath);
        }
    }
}