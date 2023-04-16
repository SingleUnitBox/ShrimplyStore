using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.Models;
using Shrimply.Models.ViewModels;

namespace ShrimplyStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShrimpController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ShrimpController(IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var shrimps = _unitOfWork.Shrimps.GetAll().ToList();

            return View(shrimps);
        }
        public IActionResult Upsert(int? id)
        {
            ShrimpViewModel shrimpViewModel = new ShrimpViewModel();
            IEnumerable<SelectListItem> SpeciesList = _unitOfWork.Species.GetAll().Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });
            shrimpViewModel.SpeciesList = SpeciesList;

            if (id == null || id == 0)
            {
                shrimpViewModel.Shrimp = new Shrimp();
            }
            else
            {
                shrimpViewModel.Shrimp = _unitOfWork.Shrimps.Get(x => x.Id == id);
                if (shrimpViewModel.Shrimp == null)
                {
                    return NotFound();
                }               
            }
            return View(shrimpViewModel);
        }
        [HttpPost]
        public IActionResult Upsert(ShrimpViewModel shrimpViewModel, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string shrimpPath = Path.Combine(wwwRootPath, @"images\shrimp");

                    if (!string.IsNullOrEmpty(shrimpViewModel.Shrimp.ImageUrl))
                    {
                        var oldImagePath =
                            Path.Combine(wwwRootPath, shrimpViewModel.Shrimp.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(shrimpPath, fileName), FileMode.Create))
                    { 
                        file.CopyTo(fileStream);
                    }
                    shrimpViewModel.Shrimp.ImageUrl = @"\images\shrimp\" + fileName;
                }

                if (shrimpViewModel.Shrimp.Id == 0)
                {
                    _unitOfWork.Shrimps.Add(shrimpViewModel.Shrimp);
                    TempData["success"] = "Shrimp created successfully.";
                }
                else
                {
                    _unitOfWork.Shrimps.Update(shrimpViewModel.Shrimp);
                    TempData["success"] = "Shrimp updated successfully.";
                }               
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(shrimpViewModel);
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
