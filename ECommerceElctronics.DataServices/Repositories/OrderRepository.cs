using ECommerceElctronics.DataServices.Data;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.DataServices.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Order>> GetAll()
        {
            return await context.Orders
                .Include(x => x.Product)
                .Include(x => x.User)
                .ToListAsync();
        }
        public override async Task<Order> GetById(int orderId)
        {
            return await context.Orders
                .Include(x => x.Product)
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == orderId);
        }
        public async Task<IEnumerable<Order>> GetOrderByUserId(int userId)
        {
            return await context.Orders
                .Include(x => x.Product)
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetOrderByCartId(int cartId)
        {
            return await context.Orders
                .Include(x => x.Product)
                .Include(x => x.User)
                .Include(x=> x.Cart)
                .Where(x => x.CartId == cartId)
                .ToListAsync();
        }
        public override async Task<bool> Update(Order entity)
        {
            var order = await GetById(entity.Id);

            if (order == null)
                return false;

            order.ProductId = entity.ProductId;
            order.UserId = entity.UserId;
            order.Quantitiy = entity.Quantitiy;
            if (entity.CartId != null)
                order.CartId = entity.CartId;

            return true;
        }
    }
}
