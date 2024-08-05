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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Cart>> GetAll()
        {
            return await context.Carts
                .Include(x => x.Orders)
                .ToListAsync();
        }
        public override async Task<Cart> GetById(int id)
        {
            return await context.Carts
                .Include(x => x.Orders)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<Cart>> GetCartByUserId(int userId)
        {
            return await context.Carts
                .Include(x => x.Orders)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
