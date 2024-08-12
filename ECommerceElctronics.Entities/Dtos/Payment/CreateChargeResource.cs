using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.Entities.Dtos.Payment
{
    public class CreateChargeResource
    {
        public string Currency { get; set; } = string.Empty;
        public long Amount { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string ReceiptEmail { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty;
    }
}
