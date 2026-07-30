namespace ECommerceElctronics.Entities.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public virtual ICollection<CartItem>? Items { get; set; } = [];
    }
}
