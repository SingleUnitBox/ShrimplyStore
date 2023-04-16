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
        public DbSet<Shrimp> Shrimps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Species>().HasData(
                new Species { Id = 1, Name = "Caridina", DisplayOrder = 1 },
                new Species { Id = 2, Name = "Neocaridina", DisplayOrder = 2 },
                new Species { Id = 3, Name = "Sulawesi", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Shrimp>().HasData(
                new Shrimp { Id = 1, Name = "Pure Red Line", Description = "PRL", BarCode = "12345", Owner = "Cez", SpeciesId = 1,
                            ListPrice = 10, Price = 5, Price50 = 4, Price100 = 3, ImageUrl = "" },
                new Shrimp { Id = 2, Name = "Pure Black Line", Description = "PBL", BarCode = "12345", Owner = "Zuk", SpeciesId = 1,
                            ListPrice = 16, Price = 12, Price50 = 10, Price100 = 8, ImageUrl = "" },
                new Shrimp { Id = 3, Name = "Pure White Line", Description = "PWL", BarCode = "12345", Owner = "Zek", SpeciesId = 2, 
                            ListPrice = 10, Price = 5, Price50 = 4, Price100 = 3, ImageUrl = "" }
                );
        }
    }
}
