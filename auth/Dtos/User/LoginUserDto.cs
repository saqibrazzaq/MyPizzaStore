using System.ComponentModel.DataAnnotations;

namespace auth.Dtos.User
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters for Username")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters for password")]
        public string? Password { get; set; }
    }
}
