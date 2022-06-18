using auth.ActionFilters;
using auth.Dtos.User;
using auth.Entities.Responses;
using auth.Services.Contractss;
using auth.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auth.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, 
            IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Login(
            [FromBody] LoginUserDto dto)
        {
            // Get login response from service
            var res = await _userService.Login(dto);
            
            // Set refresh token in cookie
            setRefreshTokenCookie(res.Data.RefreshToken);

            return Ok(res.Data);
        }

        [HttpPost("refresh-token")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RefreshToken(
            [FromBody] TokenDto dto)
        {
            // Get refresh token from http only cookie
            dto.RefreshToken = Request.Cookies[Common.RefreshTokenCookieName];

            var res = await _userService.RefreshToken(dto);

            // Set refresh token in cookie
            setRefreshTokenCookie(res.Data.RefreshToken);

            return Ok(res.Data);
        }

        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserDto dto)
        {
            await _userService.RegisterUser(dto);

            return Ok();
        }

        [HttpPost("register-admin")]
        [Authorize(Roles = Common.AdminRole)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterAdmin(
            [FromBody] RegisterUserDto dto)
        {
            await _userService.RegisterAdmin(dto);

            return Ok();
        }

        [HttpPost("delete-user")]
        [Authorize(Roles = Common.AdminRole)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteUser(
            [FromBody] DeleteUserDto dto)
        {
            await _userService.DeleteUser(dto);

            return Ok();
        }

        [HttpGet("send-verification-email")]
        [Authorize(Roles = Common.AllRoles)]
        public async Task<IActionResult> SendVerificationEmail()
        {
            await _userService.SendVerificationEmail();
            
            return Ok("Verification email sent.");
        }

        [HttpPost("verify-email")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> VerifyEmail (
            [FromBody] VerifyEmailDto dto)
        {
            await _userService.VerifyEmail(dto);

            return Ok();
        }

        [HttpGet("send-forgot-password-email")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> SendForgotPasswordEmail(
            [FromQuery] SendForgotPasswordEmailDto dto)
        {
            await _userService.SendForgotPasswordEmail(dto);

            return Ok();
        }

        [HttpPost("reset-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ResetPassword
            ([FromBody] ResetPasswordDto dto)
        {
            await _userService.ResetPassword(dto);

            return Ok();
        }

        [HttpPost("change-password")]
        [Authorize(Roles = Common.AllRoles)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ChangePassword (
            [FromBody] ChangePasswordDto dto)
        {
            await _userService.ChangePassword(dto);

            return Ok();
        }

        private void setRefreshTokenCookie(string? refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(int.Parse(
                    _configuration["JWT:RefreshTokenValidityInDays"])),
                SameSite = SameSiteMode.None,
                Secure = true
            };

            // Delete the refresh token cookie, if no token is passed
            if (string.IsNullOrEmpty(refreshToken))
            {
                Response.Cookies.Delete(Common.RefreshTokenCookieName);
            }
            else
            {
                // Set the refresh token cookie
                Response.Cookies.Append(Common.RefreshTokenCookieName, 
                    refreshToken, cookieOptions);
            }

        }

        [HttpGet("search-users")]
        [Authorize(Roles = Common.AdminRole)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> SearchUsers(
            [FromQuery] UserParameters userparameters)
        {
            var res = await _userService.SearchPersonsAsync(
                userparameters, trackChanges: false);

            return Ok(res);
        }

        [HttpGet("info")]
        [Authorize(Roles = Common.AllRoles)]
        public async Task<IActionResult> GetUser()
        {
            var res = await _userService.GetLoggedInUser();

            return Ok(res.Data);
        }

        [HttpGet]
        [Authorize(Roles = Common.AdminRole)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetUser(
            [FromQuery] GetUserRequestParams dto)
        {
            var res = await _userService.GetUser(dto);

            return Ok(res.Data);
        }
    }
}
