using Microsoft.EntityFrameworkCore;
using Shrimply.Models;

namespace Shrimply.DataAccess.Data
{
    public class ShrimplyStoreDbContext : DbContext
    {
        public ShrimplyStoreDbContext(DbContextOptions<ShrimplyStoreDbContext> options) :base(options)
        {
            
        }
        public DbSet<Species> Species { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Species>().HasData(
                new Species { Id = 1, Name = "Caridina", DisplayOrder = 1 },
                new Species { Id = 2, Name = "Neocaridina", DisplayOrder = 2 },
                new Species { Id = 3, Name = "Sulawesi", DisplayOrder = 3 }
                );
        }
    }
}
