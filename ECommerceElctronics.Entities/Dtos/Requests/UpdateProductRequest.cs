using Microsoft.AspNetCore.Http;

namespace ECommerceElctronics.Entities.Dtos.Requests
{
    public class UpdateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? Cover { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
    }
}
