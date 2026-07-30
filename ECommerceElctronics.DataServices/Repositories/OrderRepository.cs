using ECommerceElctronics.DataServices.Data;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceElctronics.DataServices.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<Order>(context), IOrderRepository
    {
        public override async Task<IEnumerable<Order>> GetAll()
        {
            return await context.Orders
                .Include(x => x.Items)
                .ToListAsync();
        }

        public override async Task<Order> GetById(int orderId)
        {
            return await context.Orders
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.Id == orderId);
        }
        public async Task<IEnumerable<Order>> GetOrderByUserId(int userId)
        {
            return await context.Orders
                .Include(x => x.Items)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        //public override async Task<bool> Update(Order entity)
        //{
            //var order = await GetById(entity.Id);

            //if (order == null)
            //    return false;

            //order.ProductId = entity.ProductId;
            //order.UserId = entity.UserId;
            //order.Quantitiy = entity.Quantitiy;
            //if (entity.CartId != null)
            //    order.CartId = entity.CartId;

            //return true;
            //return default;
        //}
    }
}
