using System.ComponentModel.DataAnnotations;

namespace auth.Dtos.User
{
    public class SendVerificationEmailDto
    {
        [Required]
        public string? UrlVerifyEmail { get; set; }
    }
}
