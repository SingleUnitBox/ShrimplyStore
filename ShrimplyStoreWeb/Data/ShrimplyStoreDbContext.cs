using Microsoft.EntityFrameworkCore;
using ShrimplyStoreWeb.Models;

namespace ShrimplyStoreWeb.Data
{
    public class ShrimplyStoreDbContext : DbContext
    {
        public ShrimplyStoreDbContext(DbContextOptions<ShrimplyStoreDbContext> options) :base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
    }
}
