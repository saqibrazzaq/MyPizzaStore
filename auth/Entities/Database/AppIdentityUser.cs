using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Entities.Database
{
    public class AppIdentityUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationTokenExpiryTime { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? ProfilePictureCloudinaryId { get; set; }

        // Foreign keys
        public Guid? AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
    }
}
