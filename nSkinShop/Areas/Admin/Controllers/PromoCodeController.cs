using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nSkinShop.DataAccess.Repository;
using nSkinShop.Models;
using nSkinShop.Utility;

namespace nSkinShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class PromoCodeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public PromoCodeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var promoCodesList = _unitOfWork.PromoCode.GetAll().ToList();
        return View(promoCodesList);
    }

    public IActionResult Upsert(int? id)
    {
        var promoCode = id == null || id == 0
            ? new PromoCode()
            : GetPromoCodeById(id);

        if (promoCode == null) return NotFound();

        return View(promoCode);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(PromoCode promoCode)
    {
        if (!ModelState.IsValid)
        {
            return View(promoCode);
        }

        if (promoCode.Id == 0)
        {
            _unitOfWork.PromoCode.Add(promoCode);
            TempData["success"] = "Promo code created successfully";
        }
        else
        {
            _unitOfWork.PromoCode.Update(promoCode);
            TempData["success"] = "Promo code updated successfully";
        }

        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int? id)
    {
        var promoCode = GetPromoCodeById(id);
        if (promoCode == null) return NotFound();

        return View(promoCode);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int? id)
    {
        var promoCode = GetPromoCodeById(id);
        if (promoCode == null) return NotFound();

        _unitOfWork.PromoCode.Remove(promoCode);
        _unitOfWork.Save();
        TempData["success"] = "Promo code deleted successfully";

        return RedirectToAction(nameof(Index));
    }
    
    private PromoCode? GetPromoCodeById(int? id)
    {
        if (id == null || id == 0) return null;
        return _unitOfWork.PromoCode.Get(p => p.Id == id);
    }
}