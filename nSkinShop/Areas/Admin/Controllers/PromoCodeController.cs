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
public class PromoCodeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public PromoCodeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public ActionResult Index()
    {
        List<PromoCode> PromoCodesList = _unitOfWork.PromoCode.GetAll().ToList();
       
        return View(PromoCodesList);
    }

    public ActionResult Upsert(int? id)
    {
        if (id == null || id == 0)
        {
            return View(new PromoCode());
        }
        else
        {
            PromoCode promoCode = _unitOfWork.PromoCode.Get(promoCode => promoCode.Id == id);
            return View(promoCode);
        }
    }

    [HttpPost]
    public ActionResult Upsert(PromoCode promoCode)
    {
        if (ModelState.IsValid)
        {
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
            return RedirectToAction("Index");
        }
        else
        {
            return View(promoCode);
        }
    }

    public ActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        PromoCode? promoCode = _unitOfWork.PromoCode.Get(promoCode => promoCode.Id == id);
        if (promoCode == null)
        {
            return NotFound();
        }

        return View(promoCode);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeletePOST(int? id)
    {
        PromoCode? promoCode = _unitOfWork.PromoCode.Get(promoCode => promoCode.Id == id);
        if (promoCode == null)
        {
            return NotFound();
        }

        _unitOfWork.PromoCode.Remove(promoCode);
        _unitOfWork.Save();
        TempData["success"] = "Promo code deleted successfully";
        return RedirectToAction("Index");
    }
}
    
