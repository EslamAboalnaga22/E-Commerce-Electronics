using ECommerceElctronics.Entities.Dtos.Account;

namespace ECommerceElctronics.DataServices.Services
{
    public interface IMailServices
    {
        Task<bool> SendMailAsync(string email, string token);
    }
}
