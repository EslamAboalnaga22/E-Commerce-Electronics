namespace ECommerceElctronics.Entities.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public DateTime? PaidAt { get; private set; }
        public DateTime? ShippedAt { get; private set; }
        public DateTime? DeliveredAt { get; private set; }
        
        public virtual ICollection<OrderItem> Items { get; set; } = [];

        public bool Pay()
        {
            if (Status != OrderStatus.Pending)
                return false;

            Status = OrderStatus.Paid;
            PaidAt = DateTime.UtcNow;

            return true;
        }

        public bool StartProcessing()
        {
            if (Status != OrderStatus.Paid)
                return false;

            Status = OrderStatus.Processing;

            return true;
        }

        public bool Ship()
        {
            if (Status != OrderStatus.Processing)
                return false;

            Status = OrderStatus.Shipped;
            ShippedAt = DateTime.UtcNow;

            return true;
        }

        public bool Deliver()
        {
            if (Status != OrderStatus.Shipped)
                return false;

            Status = OrderStatus.Delivered;
            DeliveredAt = DateTime.UtcNow;

            return true;
        }

        public bool Cancel()
        {
            if (Status == OrderStatus.Shipped ||
                Status == OrderStatus.Delivered)
                return false;

            Status = OrderStatus.Cancelled;

            return true;
        }

        public bool Refund()
        {
            if (Status != OrderStatus.Delivered)
                return false;

            Status = OrderStatus.Refunded;

            return true;
        }
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
