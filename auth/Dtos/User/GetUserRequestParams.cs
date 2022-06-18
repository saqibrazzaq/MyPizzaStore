using System.ComponentModel.DataAnnotations;

namespace auth.Dtos.User
{
    public class GetUserRequestParams
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters for Username")]
        public string? Username { get; set; }
    }
}
