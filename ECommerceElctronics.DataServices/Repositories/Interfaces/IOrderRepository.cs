using ECommerceElctronics.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.DataServices.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<IEnumerable<Order>> GetOrderByUserId(int userId);
        public Task<IEnumerable<Order>> GetOrderByCartId(int cartId);
    }
}
