using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nSkinShop.DataAccess.Repository;
using nSkinShop.Models;
using nSkinShop.Models.ViewModels;
using System.Linq;

[Area("Customer")]
[Authorize]
public class CartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

    public CartController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var shoppingCartVM = CreateShoppingCartVM();
        shoppingCartVM.OrderHeader.TotalPrice = CalculateTotalPrice(shoppingCartVM.ShoppingCartList);
        return View(shoppingCartVM);
    }

    public IActionResult Plus(int cartId)
    {
        return UpdateCartQuantity(cartId, 1);
    }

    public IActionResult Minus(int cartId)
    {
        return UpdateCartQuantity(cartId, -1);
    }

    private IActionResult UpdateCartQuantity(int cartId, int quantityChange)
    {
        var cart = _unitOfWork.ShoppingCart.Get(cart => cart.Id == cartId);
        if (cart == null) return NotFound();

        cart.Quantity += quantityChange;

        if (cart.Quantity <= 0)
        {
            _unitOfWork.ShoppingCart.Remove(cart);
        }
        else
        {
            _unitOfWork.ShoppingCart.Update(cart);
        }

        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Remove(int cartId)
    {
        var cart = _unitOfWork.ShoppingCart.Get(cart => cart.Id == cartId);
        if (cart == null) return NotFound();

        _unitOfWork.ShoppingCart.Remove(cart);
        _unitOfWork.Save();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Summary()
    {
        var shoppingCartVM = CreateShoppingCartVM();

        if (!shoppingCartVM.ShoppingCartList.Any())
        {
            TempData["error"] = "Your cart is empty. Please add products first.";
            return RedirectToAction(nameof(Index));
        }

        PopulateOrderHeader(shoppingCartVM);

        shoppingCartVM.OrderHeader.TotalPrice = CalculateTotalPrice(shoppingCartVM.ShoppingCartList);

        return View(shoppingCartVM);
    }

    public IActionResult OrderConfirmation(ShoppingCartVM shoppingCartVM)
    {
        shoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
            cart => cart.ApplicationUserId == UserId, includeProperties: "Product");

        if (!shoppingCartVM.ShoppingCartList.Any())
        {
            TempData["ErrorMessage"] = "Your cart is empty. Please add products before placing an order.";
            return RedirectToAction(nameof(Summary));
        }

        shoppingCartVM.OrderHeader ??= new OrderHeader();
        PopulateOrderHeader(shoppingCartVM);

        if (!ValidateOrderHeader(shoppingCartVM.OrderHeader))
        {
            return RedirectToAction(nameof(Summary));
        }

        _unitOfWork.OrderHeader.Add(shoppingCartVM.OrderHeader);
        _unitOfWork.Save();

        AddOrderDetails(shoppingCartVM);

        _unitOfWork.ShoppingCart.RemoveRange(shoppingCartVM.ShoppingCartList);
        _unitOfWork.Save();

        return View(shoppingCartVM.OrderHeader.Id);
    }

    private void AddOrderDetails(ShoppingCartVM shoppingCartVM)
    {
        foreach (var cartItem in shoppingCartVM.ShoppingCartList)
        {
            var orderDetail = new OrderDetail
            {
                OrderHeaderId = shoppingCartVM.OrderHeader.Id,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Price = cartItem.Product.Price
            };
            _unitOfWork.OrderDetail.Add(orderDetail);
        }
        _unitOfWork.Save();
    }

    private void PopulateOrderHeader(ShoppingCartVM shoppingCartVM)
    {
        var applicationUser = _unitOfWork.ApplicationUser.Get(user => user.Id == UserId);

        shoppingCartVM.OrderHeader.ApplicationUserId = UserId;
        shoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
        shoppingCartVM.OrderHeader.TotalPrice = CalculateTotalPrice(shoppingCartVM.ShoppingCartList);

        shoppingCartVM.OrderHeader.Name ??= applicationUser?.Name;
        shoppingCartVM.OrderHeader.PhoneNumber ??= applicationUser?.PhoneNumber;
        shoppingCartVM.OrderHeader.StreetAddress ??= applicationUser?.StreetAddress;
        shoppingCartVM.OrderHeader.City ??= applicationUser?.City;
        shoppingCartVM.OrderHeader.State ??= applicationUser?.State;
        shoppingCartVM.OrderHeader.PostalCode ??= applicationUser?.PostalCode;
    }

    private bool ValidateOrderHeader(OrderHeader orderHeader)
    {
        return !string.IsNullOrEmpty(orderHeader.PhoneNumber) &&
               !string.IsNullOrEmpty(orderHeader.StreetAddress) &&
               !string.IsNullOrEmpty(orderHeader.City) &&
               !string.IsNullOrEmpty(orderHeader.State) &&
               !string.IsNullOrEmpty(orderHeader.PostalCode) &&
               !string.IsNullOrEmpty(orderHeader.Name);
    }

    private ShoppingCartVM CreateShoppingCartVM()
    {
        return new ShoppingCartVM
        {
            ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
                cart => cart.ApplicationUserId == UserId, includeProperties: "Product") ?? new List<ShoppingCart>(),
            OrderHeader = new OrderHeader
            {
                ApplicationUser = _unitOfWork.ApplicationUser.Get(user => user.Id == UserId)
            }
        };
    }

    private double CalculateTotalPrice(IEnumerable<ShoppingCart> shoppingCartList)
    {
        return shoppingCartList.Sum(cart => cart.Quantity * cart.Product.Price);
    }
}