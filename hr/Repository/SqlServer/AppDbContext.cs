using hr.Entities;
using Microsoft.EntityFrameworkCore;

namespace hr.Repository.SqlServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        // Tables in Database context
        public DbSet<Branch>? Branches { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Designation>? Designations { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Gender>? Genders { get; set; }


    }
}
