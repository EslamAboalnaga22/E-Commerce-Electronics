using ECommerceElctronics.DataServices.Data;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceElctronics.DataServices.Repositories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
        public override async Task<bool> Update(Category category)
        {
            var result = await _dbSet.SingleOrDefaultAsync(x => x.Id == category.Id);

            if (result == null)
                return false;

            result.Name = category.Name;

            return true;
        }
    }
}

