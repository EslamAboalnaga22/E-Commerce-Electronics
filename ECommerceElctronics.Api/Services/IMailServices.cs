using ECommerceElctronics.Entities.Dtos.Account;

namespace ECommerceElctronics.Api.Services
{
    public interface IMailServices
    {
        Task<bool> SendMailAsync(string email, string token);
    }
}
