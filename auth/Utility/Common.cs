namespace auth.Utility
{
    public class Common
    {
        public const string AdminRole = "Admin";
        public const string ManagerRole = "Manager";
        public const string UserRole = "User";

        // For controller attributes
        public const string AllRoles = AdminRole + "," + ManagerRole + "," + UserRole;

        public const string RefreshTokenCookieName = "refreshToken";
    }
}
