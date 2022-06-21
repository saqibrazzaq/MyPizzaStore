namespace auth.Dtos.User
{
    public class UserDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
