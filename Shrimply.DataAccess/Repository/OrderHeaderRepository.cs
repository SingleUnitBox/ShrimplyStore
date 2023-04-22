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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ShrimplyStoreDbContext _shrimplyStoreDbContext;

        public OrderHeaderRepository(ShrimplyStoreDbContext shrimplyStoreDbContext) : base(shrimplyStoreDbContext)
        {
            _shrimplyStoreDbContext = shrimplyStoreDbContext;
        }
        public void Update(OrderHeader orderHeader)
        {
            _shrimplyStoreDbContext.OrderHeaders.Update(orderHeader);
        }
    }
}
