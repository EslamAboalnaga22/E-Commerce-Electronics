using ECommerceElctronics.DataServices.Data;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceElctronics.DataServices.Repositories
{
    public class CartItemRepository(AppDbContext context) : GenericRepository<CartItem>(context), ICartItemRepository
    {
        public async Task<int> DeletCartItems(int id)
        {
            return await context.CartItems.Where(x => x.CartId == id).ExecuteDeleteAsync();
        }
    }
}
