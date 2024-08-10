using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.Entities.Dtos.Account
{
    public class ForgetPasswordModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        //public string ClientUrl { get; set; } = string.Empty;
    }
}
