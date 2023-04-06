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
            ModelState.AddModelError("name", "bullshit");
            if (ModelState.IsValid)
            {
                _shrimplyStoreDbContext.Species.Add(species);
                _shrimplyStoreDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(species);
        }
    }
}
