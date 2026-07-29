namespace ECommerceElctronics.Entities.Dtos.Responses
{
    public class CartDtoResponse
    {
        public decimal TotalPrice { get; set; }
        public IEnumerable<CartItemsDtoResponse> Items { get; set; } = [];
    }
}
