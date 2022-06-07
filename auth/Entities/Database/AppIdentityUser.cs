using Microsoft.AspNetCore.Identity;

namespace auth.Entities.Database
{
    public class AppIdentityUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationTokenExpiryTime { get; set; }
        public string? ForgotPasswordToken { get; set; }
        public DateTime? ForgotPasswordTokenExpiryTime { get; set; }
    }
}
