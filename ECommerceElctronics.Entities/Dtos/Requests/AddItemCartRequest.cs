namespace ECommerceElctronics.Entities.Dtos.Requests
{
    public class AddItemCartRequest
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quentity { get; set; }
    }
}
