using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shrimply.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ISpeciesRepository Species { get; }
        IShrimpRepository Shrimps { get; }
        ICompanyRepository Companies { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        IApplicationUserRepository ApplicationUsers { get; }
        IOrderHeaderRepository OrderHeaders { get; }
        IOrderDetailRepository OrderDetails { get; }
        void Save();
    }
}
