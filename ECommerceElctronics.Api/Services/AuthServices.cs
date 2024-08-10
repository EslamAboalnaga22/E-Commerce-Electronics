using ECommerceElctronics.Entities.Dtos.Account;
using ECommerceElctronics.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Mail;
using System.Net;

namespace ECommerceElctronics.Api.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly JWT jwt;
        private readonly IMailServices _mailServices;

        public AuthServices(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IOptions<JWT> jwt, IMailServices mailServices)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwt = jwt.Value;
            _mailServices = mailServices;
        }
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var rolesClaims = new List<Claim>();

            foreach (var role in roles)
                rolesClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
            }
            .Union(userClaims)
            .Union(rolesClaims);

            var semmtiricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            var signingCredentials = new SigningCredentials(semmtiricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                signingCredentials: signingCredentials,
                claims: claims,
                expires: DateTime.Now.AddMinutes(3)
            );

            return jwtSecurityToken;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpireOn = DateTime.Now.AddMinutes(1),
                CreateOn = DateTime.Now,
            };
        }
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already Exist" };

            if (await userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthModel { Message = "UserName is already Exist" };

            User user = new()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in errors)
                    errors += $"{error}";

                return new AuthModel { Message = errors };
            }

            await userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                UserName = user.UserName,
                Email = user.Email,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            };
        }

        public async Task<AuthModel> LoginAsync(LoginModel model)
        {
            AuthModel authModel = new();

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            var rolesList = await userManager.GetRolesAsync(user);

            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.IsAuthenticated = true;
            authModel.Roles = rolesList.ToList();
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            if (user.RefreshTokens.Any(x => x.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(x => x.IsActive);

                authModel.RefreshToken = activeRefreshToken.Token;
                authModel.RefreshtokenExpiration = activeRefreshToken.ExpireOn;
            }
            else
            {
                var RefreshToken = GenerateRefreshToken();

                authModel.RefreshToken = RefreshToken.Token;
                authModel.RefreshtokenExpiration = RefreshToken.ExpireOn;

                user.RefreshTokens.Add(RefreshToken);

                await userManager.UpdateAsync(user);
            }

            return authModel;
        }
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user is null || !await roleManager.RoleExistsAsync(model.Role))
                return "Invalid UserId or Role";

            if (await userManager.IsInRoleAsync(user, model.Role))
                return "User is assigned to Role";

            var result = await userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Something wend wrong";
        }

        public async Task<AuthModel> RefreshTokenAsync(string token)
        {
            var authModel = new AuthModel();

            var user = await userManager.Users.SingleOrDefaultAsync(
                u => u.RefreshTokens.Any(t => t.Token == token));

            if (user is null)
            {
                authModel.Message = "invalid token";
                return authModel;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                authModel.Message = "invactive token";
                return authModel;
            }

            // We Sure that token's user in db , and is active now
            // Now we do 3 things:
            // 1- revoke token
            // 2- generate refresh token to user
            // 3- generate jwt token

            refreshToken.RevokeOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await userManager.UpdateAsync(user);

            var jwtToken = await CreateJwtToken(user);

            // Initilize authModel
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var roles = await userManager.GetRolesAsync(user);
            authModel.Roles = roles.ToList();

            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshtokenExpiration = newRefreshToken.ExpireOn;

            return authModel;
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(
                 u => u.RefreshTokens.Any(t => t.Token == token));

            if (user is null)
                return false;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokeOn = DateTime.UtcNow;

            await userManager.UpdateAsync(user);

            return true;
        }
        public async Task<bool> ChangePasswordAsync(ChangePasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return false;

            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
                return false;

            return true;
        }
        public async Task<bool> ForgetPasswordAsync(ForgetPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return false;

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            //var param = new Dictionary<string, string?>()
            //{
            //    {"token", token},
            //    {"email", model.Email}
            //};

            //var callback = QueryHelpers.AddQueryString(model.ClientUrl, param);

            var result = await _mailServices.SendMailAsync(model.Email, token);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return false;

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

            return true;
        }
    }
}
