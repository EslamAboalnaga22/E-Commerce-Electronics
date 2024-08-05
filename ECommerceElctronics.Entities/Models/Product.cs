using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } =string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty; 
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        //public decimal PriceTotal { get; set; }

        public virtual Brand? Brand { get; set; }
        public int BrandId { get; set; }
        public virtual Category? Category { get; set; }
        public int CategoryId { get; set; }

        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}
