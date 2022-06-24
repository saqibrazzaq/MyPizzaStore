using cities.Repository.Contracts;
using cities.Repository.SqlServer;
using cities.Services;
using cities.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace cities.Extensions
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

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IDataSeedService, DataSeedService>();
            services.AddScoped<IResetCityStateCountriesService, ResetCityStateCountriesService>();

            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ITimeZoneService, TimeZoneService>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(
                configuration.GetConnectionString("SqlConnection")));
        }
    }
}
