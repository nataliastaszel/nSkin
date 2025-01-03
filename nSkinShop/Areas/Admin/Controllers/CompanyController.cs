using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nSkinShop.DataAccess.Repository;
using nSkinShop.Models;
using nSkinShop.Utility;

namespace nSkinShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var companyList = _unitOfWork.Company.GetAll().ToList();
        return View(companyList);
    }

    public IActionResult Upsert(int? id)
    {
        var company = id == null || id == 0 
            ? new Company() 
            : _unitOfWork.Company.Get(c => c.Id == id) ?? new Company();

        return View(company);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Company company)
    {
        if (!ModelState.IsValid) return View(company);

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
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int? id)
    {
        var company = GetCompanyById(id);
        if (company == null) return NotFound();

        return View(company);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int? id)
    {
        var company = GetCompanyById(id);
        if (company == null) return NotFound();

        _unitOfWork.Company.Remove(company);
        _unitOfWork.Save();
        TempData["success"] = "Company deleted successfully";

        return RedirectToAction(nameof(Index));
    }
    
    private Company? GetCompanyById(int? id)
    {
        if (id == null || id == 0) return null;
        return _unitOfWork.Company.Get(c => c.Id == id);
    }
}