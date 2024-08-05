using ECommerceElctronics.Entities.Dtos.Requests;
using ECommerceElctronics.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.Entities.Dtos.Responses
{
    public class GetCartDetailsResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //public string UserName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public ICollection<GetOrderDetailssResponse> OrdersDetails { get; set; } = new List<GetOrderDetailssResponse>();
    }
}
