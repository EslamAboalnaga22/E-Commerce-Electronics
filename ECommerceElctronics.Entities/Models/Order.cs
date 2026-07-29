namespace ECommerceElctronics.Entities.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = [];
    }

    public enum OrderStatus
    {
        Pending,

        Paid,

        Processing,

        Shipped,

        Delivered,

        Cancelled,

        Refunded
    }
}
