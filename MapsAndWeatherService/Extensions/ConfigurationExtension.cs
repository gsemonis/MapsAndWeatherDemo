using Azure.Identity;
using Azure.Messaging.ServiceBus;
using MapsAndWeatherRepository.Extensions;
using MapsAndWeatherService.Interfaces;
using MapsAndWeatherService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                IHostEnvironment environment = provider.GetRequiredService<IHostEnvironment>();
                ServiceBusOptions options = provider.GetRequiredService<IOptions<ServiceBusOptions>>().Value;
                ServiceBusClientOptions clientOptions = new()
                {
                    TransportType = ServiceBusTransportType.AmqpWebSockets
                };
                if (environment.IsDevelopment())
                {
                    string connectionString = configuration.GetConnectionString("AZURE_SERVICE_BUS_CONNECTIONSTRING") ?? string.Empty;
                    return new ServiceBusClient(connectionString, clientOptions);
                }
                else
                {   
                    return new ServiceBusClient(options.FullyQualifiedNamespace, new DefaultAzureCredential(), clientOptions);                    
                }
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
