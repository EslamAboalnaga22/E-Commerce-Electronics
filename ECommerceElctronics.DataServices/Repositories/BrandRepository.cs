using ECommerceElctronics.DataServices.Data;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.DataServices.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context)
        {
        }
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
