using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.Models;

namespace ShrimplyStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShrimpController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShrimpController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Index()
        {
            var shrimps = _unitOfWork.Shrimps.GetAll().ToList();
            return View(shrimps);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Shrimp shrimp)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Shrimps.Add(shrimp);
                _unitOfWork.Save();
                TempData["success"] = "Shrimp created successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var shrimp = _unitOfWork.Shrimps.Get(x => x.Id == id);
            if (shrimp == null) 
            {
                return NotFound();
            }
            return View(shrimp);
        }
        [HttpPost]
        public IActionResult Edit(Shrimp shrimp)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Shrimps.Update(shrimp);
                _unitOfWork.Save();
                TempData["success"] = "Shrimp updated successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id) 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var shrimp = _unitOfWork.Shrimps.Get(x => x.Id == id);
            if (shrimp == null)
            {
                return NotFound();
            }
            return View(shrimp);
        }
        [HttpPost]
        public IActionResult Delete(Shrimp shrimp)
        {
            _unitOfWork.Shrimps.Remove(shrimp);
            _unitOfWork.Save();
            TempData["error"] = "Shrimp deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
