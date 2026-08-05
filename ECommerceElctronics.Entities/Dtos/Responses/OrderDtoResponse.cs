using ECommerceElctronics.Entities.Models;

namespace ECommerceElctronics.Entities.Dtos.Responses
{
    public class OrderDtoResponse
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }
        public List<OrderItemDto> Items { get; set; } = [];
    }
}
