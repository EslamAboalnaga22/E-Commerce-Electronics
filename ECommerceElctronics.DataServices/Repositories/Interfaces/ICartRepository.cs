using ECommerceElctronics.Entities.Models;

namespace ECommerceElctronics.DataServices.Repositories.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        public Task<Cart> GetCartByUserId(int userId);
    }
}
