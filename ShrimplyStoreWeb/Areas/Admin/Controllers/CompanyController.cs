using Microsoft.AspNetCore.Mvc;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.Models;

namespace ShrimplyStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companies = _unitOfWork.Companies.GetAll().ToList();
            return Json(new { data = companies });
        }
        #endregion
    }
}
