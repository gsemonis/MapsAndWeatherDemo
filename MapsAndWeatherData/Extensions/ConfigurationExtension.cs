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
            var connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING") ?? Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING") ?? throw new Exception("no connection string for db context found");

            services.AddDbContext<MapsAndWeatherContext>(options =>
                options.UseSqlServer(connectionString, sql => {
                    sql.CommandTimeout(45);
                    sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
                }));

            return services;
        }
    }
}
