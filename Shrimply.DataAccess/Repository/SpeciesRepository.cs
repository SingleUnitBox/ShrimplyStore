using Shrimply.DataAccess.Data;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shrimply.DataAccess.Repository
{
    public class SpeciesRepository : Repository<Species>, ISpeciesRepository
    {
        private readonly ShrimplyStoreDbContext _shrimplyStoreDbContext;

        public SpeciesRepository(ShrimplyStoreDbContext shrimplyStoreDbContext) : base(shrimplyStoreDbContext)
        {
            _shrimplyStoreDbContext = shrimplyStoreDbContext;
        }

        public void Update(Species species)
        {
            _shrimplyStoreDbContext.Species.Update(species);
        }
    }
}
