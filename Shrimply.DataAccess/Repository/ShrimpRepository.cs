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
            _shrimplyStoreDbContext.Shrimps.Update(shrimp);
        }
    }
}
