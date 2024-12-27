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
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public ActionResult Index()
    {
        List<Company> CompanyList = _unitOfWork.Company.GetAll().ToList();

        return View(CompanyList);
    }

    public ActionResult Upsert(int? id)
    {
        if (id == null || id == 0)
        {
            return View(new Company());
        }
        else
        {
            Company company = _unitOfWork.Company.Get(company => company.Id == id);
            return View(company);
        }
    }

    [HttpPost]
    public ActionResult Upsert(Company company)
    {
        if (ModelState.IsValid)
        {
            if (company.Id == 0)
            {
                _unitOfWork.Company.Add(company);
                TempData["success"] = "Company created successfully";
            }
            else
            {
                _unitOfWork.Company.Update(company);
                TempData["success"] = "Company updated successfully";
            }

            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        else
        {
            return View(company);
        }
    }

    public ActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Company? company = _unitOfWork.Company.Get(company => company.Id == id);
        if (company == null)
        {
            return NotFound();
        }

        return View(company);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeletePOST(int? id)
    {
        Company? company = _unitOfWork.Company.Get(company => company.Id == id);
        if (company == null)
        {
            return NotFound();
        }

        _unitOfWork.Company.Remove(company);
        _unitOfWork.Save();
        TempData["success"] = "Company deleted successfully";
        return RedirectToAction("Index");
    }
}