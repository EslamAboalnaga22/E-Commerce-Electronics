using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceElctronics.Entities.Dtos.Requests
{
    public class UpdateQuantityRequest
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
