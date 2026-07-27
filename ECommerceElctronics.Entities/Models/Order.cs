using System.Text.Json.Serialization;

namespace ECommerceElctronics.Entities.Models
{

    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int Quantitiy { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public int? CartId { get; set; }
        [JsonIgnore]
        public virtual Cart? Cart { get; set; }
    }
}
