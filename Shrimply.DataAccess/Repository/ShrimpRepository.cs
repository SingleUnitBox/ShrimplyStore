using Shrimply.DataAccess.Data;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shrimply.DataAccess.Repository
{
    public class ShrimpRepository : Repository<Shrimp>, IShrimpRepository
    {
        private readonly ShrimplyStoreDbContext _shrimplyStoreDbContext;

        public ShrimpRepository(ShrimplyStoreDbContext shrimplyStoreDbContext) : base(shrimplyStoreDbContext)
        {
            _shrimplyStoreDbContext = shrimplyStoreDbContext;
        }
        public void Update(Shrimp shrimp)
        {
            var shrimpFromDb = _shrimplyStoreDbContext.Shrimps.FirstOrDefault(x => x.Id == shrimp.Id);
            if (shrimpFromDb != null)
            {
                shrimpFromDb.Name = shrimp.Name;
                shrimpFromDb.Description = shrimp.Description;
                shrimpFromDb.BarCode = shrimp.BarCode;
                shrimpFromDb.Owner = shrimp.Owner;
                shrimpFromDb.ListPrice = shrimp.ListPrice;
                shrimpFromDb.Price = shrimp.Price;
                shrimpFromDb.Price50 = shrimp.Price50;
                shrimpFromDb.Price100 = shrimp.Price100;
                shrimpFromDb.SpeciesId = shrimp.SpeciesId;
                if (shrimpFromDb.ImageUrl != null)
                {
                    shrimpFromDb.ImageUrl = shrimp.ImageUrl;
                }
                
            }
        }
    }
}
