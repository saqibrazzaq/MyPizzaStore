﻿using auth.Dtos.User;
using auth.Entities.Database;
using auth.Entities.Exceptions;
using auth.Entities.Responses;
using auth.Services.Contractss;
using auth.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace auth.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public UserService(UserManager<AppIdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, 
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public async Task<ApiOkResponse<AuthenticationResponseDto>> Login(
            LoginUserDto dto)
        {
            // Authenticate user
            var userEntity = await AuthenticateUser(dto.Username, dto.Password);

            // If user/pwd are correct
            if (userEntity != null)
            {
                // Create response with access token
                var authRes = new AuthenticationResponseDto
                {
                    Username = dto.Username,
                    Email = userEntity.Email,
                    Roles = await _userManager.GetRolesAsync(userEntity),
                    EmailConfirmed = userEntity.EmailConfirmed
                };

                // Generate access/refresh tokens
                authRes.RefreshToken = GenerateRefreshToken();
                authRes.AccessToken = await GenerateAccessToken(userEntity);
                // Update user
                userEntity.RefreshToken = authRes.RefreshToken;
                userEntity.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                    int.Parse(_configuration["JWT:RefreshTokenValidityInDays"]));
                await _userManager.UpdateAsync(userEntity);

                return new ApiOkResponse<AuthenticationResponseDto>(authRes);
            }
            else throw new UnAuthorizedUserException("Incorrect username/password");
        }

        public async Task<ApiBaseResponse> RegisterUser(RegisterUserDto dto)
        {
            // Email and username must not already exist
            if ((await checkIfEmailAlreadyExists(dto.Email)) == true)
                throw new BadRequestException($"Email {dto.Email} is already registered. Use Forgot password if you own this account.");
            if ((await checkIfUsernameAlreadyTaken(dto.Username)) == true)
                throw new BadRequestException($"Username {dto.Username} is already taken.");

            // Create the user
            var userEntity = new AppIdentityUser
            {
                UserName = dto.Username,
                Email = dto.Email
            };
            var result = await _userManager.CreateAsync(userEntity, dto.Password);

            if (result.Succeeded == false)
                throw new BadRequestException(result.Errors.FirstOrDefault().Description);

            // Add this user in User role
            var roleResult = await _userManager.AddToRoleAsync(userEntity, Common.UserRole);
            if (roleResult.Succeeded == false)
                throw new BadRequestException(result.Errors.FirstOrDefault().Description);

            // Send email to the user
            await _emailSender.SendEmailAsync(dto.Email, "User Registration", "You can login now.");

            return new ApiBaseResponse(true);
        }

        public async Task<ApiBaseResponse> SendVerificationEmail(
            SendVerificationEmailDto dto)
        {
            // Verify email address
            var userEntity = await _userManager.FindByEmailAsync(dto.Email);
            if (userEntity == null)
                throw new NotFoundException("Email address not found.");

            // Check if email is already verified
            if (userEntity.EmailConfirmed == true)
                throw new BadRequestException("Email address already verified");

            // Create a token
            var emailVerificationToken = GenerateRefreshToken();
            
            // Update email verification token in repository
            userEntity.EmailVerificationToken = emailVerificationToken;
            userEntity.EmailVerificationTokenExpiryTime = DateTime.UtcNow.AddDays(
                    int.Parse(_configuration["JWT:EmailVerificationTokenValidityInDays"]));
            await _userManager.UpdateAsync(userEntity);
            
            var emailVerificationText = GenerateEmailVerificationText(userEntity, dto.UrlVerifyEmail);
            await _emailSender.SendEmailAsync(userEntity.Email, "Email Verification",
                emailVerificationText);

            return new ApiBaseResponse(true);
        }

        public async Task<ApiBaseResponse> VerifyEmail(VerifyEmailDto dto)
        {
            // Verify email address
            var userEntity = await _userManager.FindByEmailAsync(dto.Email);
            if (userEntity == null)
                throw new NotFoundException("Email address not found.");

            // Check if email is already verified
            if (userEntity.EmailConfirmed == true)
                throw new BadRequestException("Email address already verified");

            // Check verification token
            if (string.IsNullOrEmpty(userEntity.EmailVerificationToken))
                throw new BadRequestException("Invalid verification token");

            // Check verification token expiry
            if (userEntity.EmailVerificationTokenExpiryTime == null ||
                userEntity.EmailVerificationTokenExpiryTime < DateTime.UtcNow)
                throw new BadRequestException("Email verification token expired");

            // All checks complete, Verify email address
            userEntity.EmailConfirmed = true;
            userEntity.EmailVerificationToken = null;
            userEntity.EmailVerificationTokenExpiryTime = null;
            await _userManager.UpdateAsync(userEntity);

            return new ApiBaseResponse(true);
        }

        public async Task<ApiBaseResponse> SendForgotPasswordEmail(
            SendForgotPasswordEmailDto dto)
        {
            // Verify email address
            var userEntity = await _userManager.FindByEmailAsync(dto.Email);
            if (userEntity == null)
                throw new NotFoundException("Email address not found.");

            // Create a token
            var forgotPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(userEntity);

            // Generate forgot password email text
            string emailText = GenerateForgotPasswordEmailText(
                userEntity.Email, forgotPasswordToken, dto.UrlResetForgottenPassword);
            await _emailSender.SendEmailAsync(userEntity.Email,
                "Reset your password", emailText);

            return new ApiBaseResponse(true);
        }

        public async Task<ApiBaseResponse> ChangePassword(ChangePasswordDto dto)
        {
            // Verify email address
            var userEntity = await _userManager.FindByEmailAsync(dto.Email);
            if (userEntity == null)
                throw new NotFoundException("Email address not found.");

            // Reset password
            var result = await _userManager.ChangePasswordAsync(
                userEntity, dto.CurrentPassword, dto.NewPassword);

            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.FirstOrDefault().Description);

            return new ApiBaseResponse(true);
        }

        private string GenerateForgotPasswordEmailText(
            string email, string token, string url)
        {
            string emailText = $"Please click on the link below to reset your password. <br />" +
                $"Password reset token: {token} <br />" +
                $"<a href='{url}" +
                $"?Email={email}" +
                $"&ForgotPasswordToken={token}'>" +
                $"Reset password</a>";
            return emailText;
        }

        public async Task<ApiBaseResponse> ResetForgottenPassword(ResetForgottenPasswordDto dto)
        {
            // Verify email address
            var userEntity = await _userManager.FindByEmailAsync(dto.Email);
            if (userEntity == null)
                throw new NotFoundException("Email address not found.");

            // Update password
            var result = await _userManager.ResetPasswordAsync(
                userEntity, dto.ForgotPasswordToken, dto.Password);
            
            if (result.Succeeded == false)
                throw new BadRequestException(result.Errors.FirstOrDefault().Description);

            return new ApiBaseResponse(true);
        }

        private string GenerateEmailVerificationText(
            AppIdentityUser userEntity, string url)
        {
            string text = $"Please click on the below link to verify your email address" +
                $"<br />. <a href='{url}" +
                $"?Email={userEntity.Email}" +
                $"&verificationToken={userEntity.EmailVerificationToken}'>" +
                $"Verify email address</a>";
            return text;
        }

        private async Task<bool> checkIfEmailAlreadyExists(string? email)
        {
            var userEntity = await _userManager.FindByEmailAsync(email);
            // If email already exists, return true
            return userEntity != null ? true : false;
        }

        private async Task<bool> checkIfUsernameAlreadyTaken(string? username)
        {
            var userEntity = await _userManager.FindByNameAsync(username);
            // If username found, return true
            return userEntity != null ? true : false;
        }

        private async Task<string> GenerateAccessToken(AppIdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JWT:TokenValidityInMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<AppIdentityUser?> AuthenticateUser(string username, string password)
        {
            // Find user
            var userEntity = await _userManager.FindByNameAsync(username);
            if (userEntity == null)
                return null;

            // Check password
            var isAuthenticated = await _userManager.CheckPasswordAsync(userEntity, password);

            return isAuthenticated ? userEntity : null;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        
    }
}
