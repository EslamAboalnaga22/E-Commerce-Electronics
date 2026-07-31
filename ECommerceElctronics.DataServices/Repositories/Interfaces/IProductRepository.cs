using ECommerceElctronics.Entities.Dtos.Responses;
using ECommerceElctronics.Entities.Models;

namespace ECommerceElctronics.DataServices.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> SearchProduct(string text);
        Task<IEnumerable<Product>> FilterProductByLessPrice(decimal Price);
        Task<IEnumerable<Product>> FilterProductByBrandName(string brand);
        Task<IEnumerable<Product>> FilterProductByCategoryName(string category);
    }
}
