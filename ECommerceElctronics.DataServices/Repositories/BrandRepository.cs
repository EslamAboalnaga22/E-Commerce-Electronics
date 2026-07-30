using ECommerceElctronics.DataServices.Data;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceElctronics.DataServices.Repositories
{
    public class BrandRepository(AppDbContext context) : GenericRepository<Brand>(context), IBrandRepository
    {
        public override async Task<bool> Update(Brand brand)
        {
            var result = await _dbSet.SingleOrDefaultAsync(x => x.Id == brand.Id);

            if (result == null) 
                return false;

            result.Name = brand.Name;

            return true;
        }

    }
}
