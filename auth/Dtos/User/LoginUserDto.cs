using System.ComponentModel.DataAnnotations;

namespace auth.Dtos.User
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(255, ErrorMessage = "Maximum 255 characters for Email")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters for password")]
        public string? Password { get; set; }
    }
}
