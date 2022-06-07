using auth.Entities.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace auth.Repository.SqlServer
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        // Tables in Database context
        public DbSet<AppIdentityUser>? Users { get; set; }
    }
}
