using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerceElctronics.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public virtual Cart? Cart { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}
