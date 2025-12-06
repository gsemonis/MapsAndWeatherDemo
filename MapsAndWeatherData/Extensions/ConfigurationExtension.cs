using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MapsAndWeatherData.Extensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services,
            IConfiguration configuration)
        {           
            var connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            services.AddDbContext<MapsAndWeatherContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
