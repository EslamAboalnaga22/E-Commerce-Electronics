using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.Entities.Dtos.Responses
{
    public class CartDtoResponse
    {
        public decimal TotoalPrice { get; set; }
        public IEnumerable<CartItemDtoResponse> Items { get; set; } = [];
    }
}
