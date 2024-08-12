using ECommerceElctronics.Entities.Dtos.Account;
using ECommerceElctronics.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.DataServices.Services
{
    public interface IAuthServices
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
        Task<bool> ChangePasswordAsync(ChangePasswordModel model);
        Task<bool> ForgetPasswordAsync(ForgetPasswordModel model);
        Task<bool> ResetPasswordAsync(ResetPasswordModel model);
    }
}
