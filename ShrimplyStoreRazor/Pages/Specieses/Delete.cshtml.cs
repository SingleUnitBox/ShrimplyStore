using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShrimplyStoreRazor.Data;
using ShrimplyStoreRazor.Models;

namespace ShrimplyStoreRazor.Pages.Specieses
{
    public class DeleteModel : PageModel
    {
        private readonly ShrimplyStoreRazorDbContext _db;

        public DeleteModel(ShrimplyStoreRazorDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Species Species { get; set; }
        public IActionResult OnGet(int? id)
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
            _db.Species.Remove(Species);
            _db.SaveChanges();
            TempData["error"] = "Species deleted successfully";
            return RedirectToPage("Index");
        }
    }
}
