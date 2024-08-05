using ECommerceElctronics.DataServices.Data;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using ECommerceElctronics.Entities.Dtos.Responses;


namespace ECommerceElctronics.DataServices.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
           

        }

        public override async Task<IEnumerable<Product>> GetAll()
        {
            return await context.Products
                .Include(x=> x.Brand)
                .Include(x=> x.Category)
                .ToListAsync();
        }

        public override async Task<Product> GetById(int id)
        {
            return await context.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
        public override async Task<bool> Update(Product entity)
        {
           var product = await GetById(entity.Id);

            if (product == null) 
                return false;

            product.Name = entity.Name;
            product.Description = entity.Description;
            product.Price = entity.Price;
            product.Discount = entity.Discount;
            product.BrandId = entity.BrandId;
            product.CategoryId = entity.CategoryId;
            product.Image = entity.Image;

            return true;
        }
    }
}

