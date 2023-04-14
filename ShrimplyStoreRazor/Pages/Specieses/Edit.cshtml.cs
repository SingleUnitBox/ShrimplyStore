using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShrimplyStoreRazor.Data;
using ShrimplyStoreRazor.Models;

namespace ShrimplyStoreRazor.Pages.Specieses
{
    public class EditModel : PageModel
    {
        private readonly ShrimplyStoreRazorDbContext _db;

        public EditModel(ShrimplyStoreRazorDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Species Species { get; set; }
        public IActionResult OnGet(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Species = _db.Species.Find(id);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Species.Update(Species);
                _db.SaveChanges();
                TempData["success"] = "Species edited successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
