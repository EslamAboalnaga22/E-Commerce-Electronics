using ECommerceElctronics.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerceElctronics.Entities.Dtos.Requests
{
    public class UpdateOrderRequest
    {
        public int ProductId { get; set; }
        public int Quantitiy { get; set; }
        public int UserId { get; set; }
        public int? CartId { get; set; }
    }
}
