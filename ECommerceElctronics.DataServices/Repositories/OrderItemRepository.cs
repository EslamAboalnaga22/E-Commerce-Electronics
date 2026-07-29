using ECommerceElctronics.DataServices.Data;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;

namespace ECommerceElctronics.DataServices.Repositories
{
    public class OrderItemRepository(AppDbContext context) : GenericRepository<OrderItem>(context), IOrderItemRepository
    {
    }
}
