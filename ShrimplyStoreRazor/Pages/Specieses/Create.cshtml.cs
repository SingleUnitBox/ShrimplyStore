using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShrimplyStoreRazor.Data;
using ShrimplyStoreRazor.Models;

namespace ShrimplyStoreRazor.Pages.Specieses
{
    public class CreateModel : PageModel
    {
        private readonly ShrimplyStoreRazorDbContext _db;

        public CreateModel(ShrimplyStoreRazorDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Species Species { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            _db.Species.Add(Species);
            _db.SaveChanges();
            TempData["success"] = "Species created successfully";
            return RedirectToPage("Index");
        }
    }
}
