using auth.Entities.Database;
using auth.Services.Contractss;
using auth.Utility;
using Microsoft.AspNetCore.Identity;

namespace auth.Services
{
    public class DataSeedService : IDataSeedService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public DataSeedService(
            RoleManager<IdentityRole> roleManager,
            UserManager<AppIdentityUser> userManager, 
            IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task AddDefaultRolesAndUsers()
        {
            // Default roles which should exist
            var roleNames = new List<string>() { 
                Common.AdminRole, Common.ManagerRole, Common.UserRole };
            foreach (var roleName in roleNames)
            {
                // Add role, if it does not already exist
                var roleEntity = await _roleManager.FindByNameAsync(roleName);
                if (roleEntity == null)
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Add admin user
            var username = _configuration["DefaultUser:Username"];
            var email = _configuration["DefaultUser:Email"];
            var password = _configuration["DefaultUser:Password"];

            var userEntity = await _userManager.FindByNameAsync(username);
            if (userEntity == null)
            {
                var res = await _userManager.CreateAsync(new AppIdentityUser
                {
                    UserName = username,
                    Email = email
                }, password);
                if (res.Succeeded)
                {
                    userEntity = await _userManager.FindByNameAsync(username);
                    await _userManager.AddToRoleAsync(userEntity, Common.AdminRole);
                }
            }
        }
    }
}
