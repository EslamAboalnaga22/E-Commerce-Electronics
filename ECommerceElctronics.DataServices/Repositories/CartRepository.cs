using ECommerceElctronics.DataServices.Data;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceElctronics.DataServices.Repositories
{
    public class CartRepository(AppDbContext context) : GenericRepository<Cart>(context), ICartRepository
    {
        public override async Task<IEnumerable<Cart>> GetAll()
        {
            return await context.Carts
                .Include(x => x.Items)
                .ToListAsync();
        }

        public override async Task<Cart> GetById(int id)
        {
            return await context.Carts
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Cart> GetCartByUserId(int userId)
        {
            return await context.Carts
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
