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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ShrimplyStoreDbContext _shrimplyStoreDbContext;

        public OrderDetailRepository(ShrimplyStoreDbContext shrimplyStoreDbContext) : base(shrimplyStoreDbContext)
        {
            _shrimplyStoreDbContext = shrimplyStoreDbContext;
        }

        public void Update(OrderDetail orderDetail)
        {
            _shrimplyStoreDbContext.OrderDetails.Update(orderDetail);
        }
    }
}
