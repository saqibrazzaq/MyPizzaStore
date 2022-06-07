using System.Text.Json.Serialization;

namespace auth.Dtos.User
{
    public class AuthenticationResponseDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public IEnumerable<string>? Roles { get; set; }
        public string? AccessToken { get; set; }
        public bool EmailConfirmed { get; set; } = false;

        [JsonIgnore]
        public string? RefreshToken { get; set; }
    }
}
