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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ShrimplyStoreDbContext _shrimplyStoreDbContext;

        public ShoppingCartRepository(ShrimplyStoreDbContext shrimplyStoreDbContext) : base(shrimplyStoreDbContext)
        {
            _shrimplyStoreDbContext = shrimplyStoreDbContext;
        }
        public void Update(ShoppingCart shoppingCart)
        {
            _shrimplyStoreDbContext.Update(shoppingCart);
        }
    }
}
