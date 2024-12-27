using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using nSkinShop.DataAccess.Repository;
using nSkinShop.Models;
using nSkinShop.Models.ViewModels;
using nSkinShop.Utility;

namespace nSkinShop;

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

    public ActionResult Index()
    {
        List<Product> ProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
       
        return View(ProductList);
    }

    public ActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            CategoryList = _unitOfWork.Category.GetAll().Select(category => new SelectListItem
            {
                Text = category.Name,
                Value = category.Id.ToString()
            }),
            Product = new Product()
        };
        if (id == null || id == 0)
        {
            return View(productVM);
        }
        else
        {
            productVM.Product = _unitOfWork.Product.Get(product => product.Id == id);
            return View(productVM);
        }
    }

    [HttpPost]
    public ActionResult Upsert(ProductVM productVM, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            if (productVM.Product.Id == 0)
            {
                _unitOfWork.Product.Add(productVM.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productVM.Product);
            }


            string wwwRootPath = _webHostEnvironment.WebRootPath;
            
            if (file != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, "images", "product");
                
                if (!Directory.Exists(productPath))
                {
                    Directory.CreateDirectory(productPath);
                }
                
                if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart(Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                
                string filePath = Path.Combine(productPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                
                productVM.Product.ImageUrl = Path.Combine("images", "product", fileName).Replace(Path.DirectorySeparatorChar, '/');
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
            return RedirectToAction("Index");
        }
        else
        {
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(category => new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            }
            ;
            return View(productVM);
        }
    }

    public ActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Product? product = _unitOfWork.Product.Get(product => product.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeletePOST(int? id)
    {
        Product? product = _unitOfWork.Product.Get(product => product.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        
        if (!string.IsNullOrEmpty(product.ImageUrl))
        {
            var imagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart(Path.DirectorySeparatorChar));
            
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
        
        _unitOfWork.Product.Remove(product);
        _unitOfWork.Save();
        TempData["success"] = "Product deleted successfully";
        return RedirectToAction("Index");
        
    }
    
}