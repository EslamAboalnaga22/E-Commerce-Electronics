using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.Entities.Dtos.Requests
{
    public class CreateCartRequest
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CreateOrderRequest> OrdersRequest { get; set; } = new List<CreateOrderRequest>();

    }
}
