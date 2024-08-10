using ECommerceElctronics.Api.Services;
using ECommerceElctronics.Entities.Dtos.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceElctronics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly IMailServices _mailServices;

        public AuthController(IAuthServices authServices, IMailServices mailServices)
        {
            _authServices = authServices;
            _mailServices = mailServices;
        }
        private void SetRefreshTokenInCookies(string refreshToken, DateTime expires)
        {
            var cookiesOption = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime()
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookiesOption);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var result = await _authServices.RegisterAsync(model);

            if(!result.IsAuthenticated)
                return BadRequest(result.Message);

            SetRefreshTokenInCookies(result.RefreshToken, result.RefreshtokenExpiration);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authServices.LoginAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            if(!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookies(result.RefreshToken, result.RefreshtokenExpiration);

            return Ok(result);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authServices.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _authServices.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result);

            SetRefreshTokenInCookies(result.RefreshToken, result.RefreshtokenExpiration);

            return Ok(result);
        }

        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken(RevokeTokenModel model)
        {
            var token = model.Token;

            if (string.IsNullOrEmpty(token))
                token = Request.Cookies["refreshToken"];
            else
                return BadRequest("Token is Required");

            var result = await _authServices.RevokeTokenAsync(token);

            if (!result)
                return BadRequest("Token is Invalid");

            return Ok();
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authServices.ChangePasswordAsync(model);

            if (result == false)
                return BadRequest(result);

            return Ok();
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authServices.ForgetPasswordAsync(model);

            if (result == false)
                return BadRequest(result);

            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authServices.ResetPasswordAsync(model);

            if (result == false)
                return BadRequest(result);

            return Ok();
        }
    }
}
