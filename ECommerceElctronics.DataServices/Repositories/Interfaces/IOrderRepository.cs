using ECommerceElctronics.Entities.Models;

namespace ECommerceElctronics.DataServices.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<IEnumerable<Order>> GetOrderByUserId(int userId);
    }
}
