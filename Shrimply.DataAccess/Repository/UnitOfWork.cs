using Shrimply.DataAccess.Data;
using Shrimply.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shrimply.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShrimplyStoreDbContext _shrimplyStoreDbContext;
        public ISpeciesRepository Species { get; private set; }
        public IShrimpRepository Shrimps { get; private set; }
        public ICompanyRepository Companies { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; private set; }
        public IApplicationUserRepository ApplicationUsers { get; private set; }
        public IOrderHeaderRepository OrderHeaders { get; private set; }
        public IOrderDetailRepository OrderDetails { get; set; }

        public UnitOfWork(ShrimplyStoreDbContext shrimplyStoreDbContext)
        {
            _shrimplyStoreDbContext = shrimplyStoreDbContext;
            Species = new SpeciesRepository(_shrimplyStoreDbContext);
            Shrimps = new ShrimpRepository(_shrimplyStoreDbContext);
            Companies = new CompanyRepository(_shrimplyStoreDbContext);
            ShoppingCarts = new ShoppingCartRepository(_shrimplyStoreDbContext);
            ApplicationUsers = new ApplicationUserRepository(_shrimplyStoreDbContext);
            OrderHeaders = new OrderHeaderRepository(_shrimplyStoreDbContext);
            OrderDetails = new OrderDetailRepository(_shrimplyStoreDbContext);
        }
        
        public void Save()
        {
            _shrimplyStoreDbContext.SaveChanges();
        }
    }
}
