using System.ComponentModel.DataAnnotations;

namespace auth.Dtos.User
{
    public class VerifyEmailDto
    {
        [Required]
        public string? PinCode { get; set; }
        

    }
}
