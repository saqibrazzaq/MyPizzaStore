using System.ComponentModel.DataAnnotations;

namespace auth.Dtos.User
{
    public class SendForgotPasswordEmailDto
    {
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(255, ErrorMessage = "Maximum 255 characters for Email")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? UrlResetForgottenPassword { get; set; }
    }
}
