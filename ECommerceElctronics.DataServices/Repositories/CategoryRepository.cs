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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
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

