using ECommerceElctronics.DataServices.Data;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace ECommerceElctronics.DataServices.Repositories
{
    public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
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

