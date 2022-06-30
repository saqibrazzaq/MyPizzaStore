using hr.Repository.Contracts;
using hr.Repository.SqlServer;
using hr.Services;
using hr.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace hr.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                    //.AllowAnyOrigin()
                    .WithOrigins("https://localhost:3000")
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination");
                });
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void MigrateDatabase(this IServiceCollection services)
        {
            var dbContext = services.BuildServiceProvider().GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }

        public static void SeedDefaultData(this IServiceCollection services)
        {
            var dataSeeder = services.BuildServiceProvider().GetRequiredService<IDataSeedService>();
            dataSeeder.SeedDefaultData();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IDataSeedService, DataSeedService>();
            services.AddScoped<ICompanyService, CompanyService>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(
                configuration.GetConnectionString("SqlConnection")));
        }
    }
}
