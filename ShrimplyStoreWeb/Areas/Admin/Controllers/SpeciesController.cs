using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shrimply.DataAccess.Data;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.Models;
using Shrimply.Utility;
using System.Data;

namespace ShrimplyStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SpeciesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpeciesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var speciesList = _unitOfWork.Species.GetAll().ToList();
            return View(speciesList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Species species)
        {
            //ModelState.AddModelError("name", "test message");
            if (ModelState.IsValid)
            {
                _unitOfWork.Species.Add(species);
                _unitOfWork.Save();
                TempData["success"] = "Species created successfully.";
                return RedirectToAction("Index");
            }
            return View(species);
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var species = _unitOfWork.Species.Get(u => u.Id == id);
            if (species == null)
            {
                return NotFound();
            }
            return View(species);
        }

        [HttpPost]
        public IActionResult Edit(Species species)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Species.Update(species);
                _unitOfWork.Save();
                TempData["success"] = "Species edited successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var species = _unitOfWork.Species.Get(x => x.Id == id);
            if (species == null)
            {
                return NotFound();
            }
            return View(species);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var species = _unitOfWork.Species.Get(x => x.Id == id);
            if (species == null)
            {
                return NotFound();
            }
            _unitOfWork.Species.Remove(species);
            _unitOfWork.Save();
            TempData["success"] = "Species deleted successfully.";
            return RedirectToAction("Index");

        }
    }
}
