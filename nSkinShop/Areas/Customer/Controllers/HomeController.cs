using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nSkinShop.DataAccess.Repository;
using nSkinShop.Models;

namespace nSkinShop.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return View(productList);
    }

    public IActionResult Details(int productId)
    {
        var product = _unitOfWork.Product.Get(p => p.Id == productId, includeProperties: "Category");

        if (product == null)
        {
            return NotFound();
        }

        var shoppingCart = new ShoppingCart
        {
            Product = product,
            ProductId = productId,
            Quantity = 1
        };

        return View(shoppingCart);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult Details(ShoppingCart shoppingCart)
    {
        if (!ModelState.IsValid)
        {
            return View(shoppingCart);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        shoppingCart.ApplicationUserId = userId;

        var existingCart = _unitOfWork.ShoppingCart.Get(cart =>
            cart.ApplicationUserId == userId && cart.ProductId == shoppingCart.ProductId);

        if (existingCart != null)
        {
            existingCart.Quantity += shoppingCart.Quantity;
            _unitOfWork.ShoppingCart.Update(existingCart);
        }
        else
        {
            _unitOfWork.ShoppingCart.Add(shoppingCart);
        }

        _unitOfWork.Save();
        TempData["success"] = "Product added to cart";

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}