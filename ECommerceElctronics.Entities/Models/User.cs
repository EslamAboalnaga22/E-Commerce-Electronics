
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerceElctronics.Entities.Models
{
    public class User : IdentityUser<int> 
    {
        // public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual Cart? Cart { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
