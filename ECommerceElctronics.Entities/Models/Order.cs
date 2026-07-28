using System.Text.Json.Serialization;

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
        //public int Id { get; set; }
        //public int ProductId { get; set; }
        //public virtual Product? Product { get; set; }
        //public int Quantitiy { get; set; }
        //public int UserId { get; set; }
        //public virtual User? User { get; set; }
        //public int? CartId { get; set; }
        //[JsonIgnore]
        //public virtual Cart? Cart { get; set; }

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
