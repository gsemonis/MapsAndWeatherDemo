using Azure.Identity;
using Azure.Messaging.ServiceBus;
using MapsAndWeatherRepository.Extensions;
using MapsAndWeatherService.Interfaces;
using MapsAndWeatherService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MapsAndWeatherService.Extensions
{
    internal class ServiceBusOptions
    {
        public string FullyQualifiedNamespace { get; set; } = string.Empty;
        public string QueueName { get; set; } = string.Empty;
    }
    public static class ConfigurationExtension
    {
        public static IServiceCollection ConfigureMapsAndWeatherServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.ConfigureMapsAndWeatherRepositories(configuration);
            services.Configure<ServiceBusOptions>(configuration.GetSection("AzureServiceBus"));
            services.AddSingleton<ServiceBusClient>((provider) =>             
            {      
                ServiceBusOptions options = provider.GetRequiredService<IOptions<ServiceBusOptions>>().Value; 
                ServiceBusClientOptions clientOptions = new()
                {
                    TransportType = ServiceBusTransportType.AmqpWebSockets
                };
                ServiceBusClient ret = new(options.FullyQualifiedNamespace, new DefaultAzureCredential(), clientOptions);
                return ret;                
            });
            services.AddSingleton<ServiceBusSender>((provider) => {
                ServiceBusOptions options = provider.GetRequiredService<IOptions<ServiceBusOptions>>().Value;
                ServiceBusClient client = provider.GetRequiredService<ServiceBusClient>();
                return client.CreateSender(options.QueueName);
            });
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ILogService, LogService>();   
            
            return services;
        }
    }
}
