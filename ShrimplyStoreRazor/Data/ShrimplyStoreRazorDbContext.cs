using Microsoft.EntityFrameworkCore;
using ShrimplyStoreRazor.Models;

namespace ShrimplyStoreRazor.Data
{
    public class ShrimplyStoreRazorDbContext : DbContext
    {
        public ShrimplyStoreRazorDbContext(DbContextOptions<ShrimplyStoreRazorDbContext> options) : base(options)
        {
            
        }
        public DbSet<Species> Species { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Species>().HasData(
                new Species { Id = 1, Name = "Caridina", DisplayOrder = 1},
                new Species { Id = 2, Name = "Neo", DisplayOrder = 2},
                new Species { Id = 3, Name = "Sula", DisplayOrder = 3}
                );
        }
    }
}
