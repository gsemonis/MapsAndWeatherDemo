using MapsAndWeatherData.Extensions;
using MapsAndWeatherRepository.Interfaces;
using MapsAndWeatherRepository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MapsAndWeatherRepository.Extensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection ConfigureMapsAndWeatherRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // Add service configurations here in the future
            services.AddDataAccess(configuration);
            services.AddMemoryCache();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            return services;
        }
    }
}
