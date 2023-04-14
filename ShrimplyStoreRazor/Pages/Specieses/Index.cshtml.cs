using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShrimplyStoreRazor.Data;
using ShrimplyStoreRazor.Models;

namespace ShrimplyStoreRazor.Pages.Specieses
{
    public class IndexModel : PageModel
    {
        private readonly ShrimplyStoreRazorDbContext _db;
        public List<Species> Species { get; set; }
        public IndexModel(ShrimplyStoreRazorDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Species = _db.Species.ToList();
        }
    }
}
