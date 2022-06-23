using cities.Entities;
using Microsoft.EntityFrameworkCore;

namespace cities.Repository.SqlServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        // Tables in Database context
        public DbSet<Country>? Countries { get; set; }
        public DbSet<Entities.TimeZone>? TimeZones { get; set; }
        public DbSet<State>? States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // State table composite key
            modelBuilder.Entity<State>()
                .HasKey(x => new { x.StateCode, x.CountryCode });
        }
    }
}
