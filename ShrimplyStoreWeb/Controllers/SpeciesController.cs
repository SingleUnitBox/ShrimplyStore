using Microsoft.AspNetCore.Mvc;
using ShrimplyStoreWeb.Data;
using ShrimplyStoreWeb.Models;

namespace ShrimplyStoreWeb.Controllers
{
    public class SpeciesController : Controller
    {
        private readonly ShrimplyStoreDbContext _shrimplyStoreDbContext;

        public SpeciesController(ShrimplyStoreDbContext shrimplyStoreDbContext)
        {
            _shrimplyStoreDbContext = shrimplyStoreDbContext;
        }
        public IActionResult Index()
        {
            var speciesList = _shrimplyStoreDbContext.Species.ToList();
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
                _shrimplyStoreDbContext.Species.Add(species);
                _shrimplyStoreDbContext.SaveChanges();
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
            var species = _shrimplyStoreDbContext.Species.FirstOrDefault(x => x.Id == id);
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
                _shrimplyStoreDbContext.Species.Update(species);
                _shrimplyStoreDbContext.SaveChanges();
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
            var species = _shrimplyStoreDbContext.Species.FirstOrDefault(x => x.Id == id);
            if (species == null)
            {
                return NotFound();
            }
            return View(species);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var species = _shrimplyStoreDbContext.Species.Find(id);
            if (species == null)
            {
                return NotFound();
            }
            _shrimplyStoreDbContext.Species.Remove(species);
            _shrimplyStoreDbContext.SaveChanges();
            TempData["success"] = "Species deleted successfully.";
            return RedirectToAction("Index");

        }
    }
}
