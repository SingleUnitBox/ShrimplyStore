using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.Models;
using Shrimply.Models.ViewModels;
using Shrimply.Utility;

namespace ShrimplyStoreWeb.Areas.Admin.Controllers
{
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
            IEnumerable<Company> companies = _unitOfWork.Companies.GetAll().ToList();
            return View(companies);
        }
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Company());
            }
            else
            {
                var company = _unitOfWork.Companies.Get(x => x.Id == id);
                if (company == null)
                {
                    return NotFound();
                }
                return View(company);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitOfWork.Companies.Add(company);
                    TempData["success"] = "Company created successfully.";
                }
                else
                {
                    _unitOfWork.Companies.Update(company);
                    TempData["success"] = "Company updated successfully.";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(company);
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companies = _unitOfWork.Companies.GetAll().ToList();
            return Json(new { data = companies });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.Companies.Get(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Companies.Remove(companyToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
