using ECommerceElctronics.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.DataServices.Repositories.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        public Task<IEnumerable<Cart>> GetCartByUserId(int userId);
    }
}
