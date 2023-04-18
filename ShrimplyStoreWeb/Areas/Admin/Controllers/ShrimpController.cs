using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.Models;
using Shrimply.Models.ViewModels;
using Shrimply.Utility;

namespace ShrimplyStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
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
            var shrimps = _unitOfWork.Shrimps.GetAll(includeProperties: "Species").ToList();

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

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() 
        {
            var shrimps = _unitOfWork.Shrimps.GetAll(includeProperties: "Species").ToList();
            return Json(new { data = shrimps });
        }
        [HttpDelete]
        public IActionResult Delete(int? id) 
        {
            var shrimpToBeDeleted = _unitOfWork.Shrimps.Get(u => u.Id == id);
            if (shrimpToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(wwwRootPath, shrimpToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Shrimps.Remove(shrimpToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
