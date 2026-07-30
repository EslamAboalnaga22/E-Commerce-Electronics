using ECommerceElctronics.Entities.Models;

namespace ECommerceElctronics.Entities.Dtos.Responses
{
    public class CartItemsDtoResponse
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
