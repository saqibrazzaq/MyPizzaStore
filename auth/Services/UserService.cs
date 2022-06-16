using auth.Dtos;
using auth.Dtos.User;
using auth.Entities.Database;
using auth.Entities.Exceptions;
using auth.Entities.Responses;
using auth.Repository.Contracts;
using auth.Services.Contractss;
using auth.Utility;
using AutoMapper;
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
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserService(UserManager<AppIdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IEmailSender emailSender,
            IRepositoryManager repository,
            IMapper mapper, 
            IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _repository = repository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ApiOkResponse<AuthenticationResponseDto>> Login(
            LoginUserDto dto)
        {
            // Authenticate user
            var userEntity = await AuthenticateUser(dto.Email, dto.Password);

            // If user/pwd are correct
            if (userEntity != null)
            {
                // Create response with access token
                var authRes = new AuthenticationResponseDto
                {
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

        public async Task<ApiOkResponse<TokenDto>> RefreshToken(TokenDto dto)
        {
            var principal = GetPrincipalFromExpiredToken(dto.AccessToken);
            if (principal == null)
                throw new BadRequestException("Invalid access token or refresh token");

            string username = principal.Identity.Name;

            var userEntity = await _userManager.FindByNameAsync(username);

            if (userEntity == null || userEntity.RefreshToken != dto.RefreshToken 
                || userEntity.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new BadRequestException("Invalid access token or refresh token");
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            // Update user repository
            userEntity.RefreshToken = newRefreshToken;
            userEntity.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                   int.Parse(_configuration["JWT:RefreshTokenValidityInDays"]));
            var userResult = await _userManager.UpdateAsync(userEntity);
            if (userResult.Succeeded == false)
                throw new BadRequestException("Invalid token");

            return new ApiOkResponse<TokenDto>(new TokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
            });
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

        public async Task<ApiBaseResponse> RegisterUser(RegisterUserDto dto)
        {
            await CheckExistingEmailAndUsername(dto);

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

            await SendVerificationEmail(new SendVerificationEmailDto
            {
                UrlVerifyEmail = dto.UrlVerifyEmail
            });

            return new ApiBaseResponse(true);
        }

        public async Task<ApiBaseResponse> RegisterAdmin(RegisterUserDto dto)
        {
            await CheckExistingEmailAndUsername(dto);

            // Create new user
            var userEntity = new AppIdentityUser
            {
                UserName = dto.Username,
                Email = dto.Email
            };
            var resultUser = await _userManager.CreateAsync(userEntity, dto.Password);
            if (resultUser.Succeeded == false)
                throw new BadRequestException(resultUser.Errors.FirstOrDefault().Description);

            // Add default roles if do not exist
            await AddDefaultRoles();

            // Assign admin role
            var roleResult = await _userManager.AddToRoleAsync(userEntity, Common.AdminRole);
            if (roleResult.Succeeded == false)
                throw new BadRequestException(roleResult.Errors.FirstOrDefault().Description);

            SendVerificationEmail(new SendVerificationEmailDto
            {
                UrlVerifyEmail = dto.UrlVerifyEmail
            });

            return new ApiBaseResponse(true);
        }

        public async Task<ApiBaseResponse> DeleteUser(DeleteUserDto dto)
        {
            var userEntity = await _userManager.FindByNameAsync(dto.Username);
            if (userEntity == null)
                throw new BadRequestException("User does not exist");
            else
            {
                var resultUser = await _userManager.DeleteAsync(userEntity);
                if (resultUser.Succeeded == false)
                    throw new BadRequestException(resultUser.Errors.FirstOrDefault().Description);
            }

            return new ApiBaseResponse(true);
        }

        private async Task AddDefaultRoles()
        {
            // Get all roles
            var allRoleNames = Common.AllRoles.Split(',');

            foreach (var roleName in allRoleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    if (roleResult.Succeeded == false)
                        throw new BadRequestException(roleResult.Errors.FirstOrDefault().Description);
                }
            }
        }

        private async Task CheckExistingEmailAndUsername(RegisterUserDto dto)
        {
            // Email and username must not already exist
            if ((await checkIfEmailAlreadyExists(dto.Email)) == true)
                throw new BadRequestException($"Email {dto.Email} is already registered. Use Forgot password if you own this account.");
            if ((await checkIfUsernameAlreadyTaken(dto.Username)) == true)
                throw new BadRequestException($"Username {dto.Username} is already taken.");
        }

        public async Task<ApiBaseResponse> SendVerificationEmail(
            SendVerificationEmailDto dto)
        {
            // Verify email address
            var userEntity = await _userManager.FindByNameAsync(UserName);
            if (userEntity == null)
                throw new NotFoundException("User not found.");

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

            var token = CreateToken(authClaims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private async Task<AppIdentityUser?> AuthenticateUser(string email, string password)
        {
            // Find user
            var userEntity = await _userManager.FindByEmailAsync(email);
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

        public async Task<ApiOkPagedResponse<IEnumerable<UserDto>, MetaData>> SearchPersonsAsync(UserParameters userParameters, bool trackChanges)
        {
            var usersWithMetadata = await _repository.UserRepository.SearchUsersAsync(
                userParameters, trackChanges);
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(usersWithMetadata);
            return new ApiOkPagedResponse<IEnumerable<UserDto>, MetaData>(
                usersDto, usersWithMetadata.MetaData);
        }

        public async Task<ApiOkResponse<UserDto>> GetUser()
        {
            //_userManager.Get
            var userEntity = await _userManager.FindByNameAsync(UserName);
            if (userEntity == null)
                throw new NotFoundException("User not found");

            var userDto = _mapper.Map<UserDto>(userEntity);
            return new ApiOkResponse<UserDto>(userDto);
        }

        private string UserName
        {
            get
            {
                return _contextAccessor.HttpContext.User.Identity.Name;
            }
        }
    }
}
