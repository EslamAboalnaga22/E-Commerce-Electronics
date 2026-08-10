using ECommerceElctronics.DataServices.Data;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using static System.Net.Mime.MediaTypeNames;


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

        public async Task<IEnumerable<Product>> SearchProduct(string text)
        {
            return await context.Products
                .AsNoTracking()
                .Where(x=> x.Name.Contains(text))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FilterProductByLessPrice(decimal Price)
        {
            return await context.Products
                .AsNoTracking()
                .Where(x => x.Price <= Price)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FilterProductByBrandName(string brand)
        {
            return await context.Products
                .Include(x => x.Brand)
                .AsNoTracking()
                .Where(x => x.Brand!.Name == brand)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FilterProductByCategoryName(string category)
        {
            return await context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .Where(x => x.Category!.Name == category)
                .ToListAsync();
        }
    }
}

