using ECommerceElctronics.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.Entities.Dtos.Responses
{
    public class GetOrderDetailssResponse
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; } = string.Empty;
        public int Quantitiy { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int? CartId { get; set; }
        //public virtual Cart? Cart { get; set; }
    }
}
