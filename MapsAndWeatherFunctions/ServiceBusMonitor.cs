using Azure.Messaging.ServiceBus;
using MapsAndWeatherData;
using MapsAndWeatherData.Models;
using Microsoft.Azure.Functions.Worker;
using System.Text.Json;

namespace MapsAndWeatherFunctions;

public class ServiceBusMonitor(MapsAndWeatherContext context)
{
    private static JsonSerializerOptions options = new(JsonSerializerDefaults.Web);
    [Function(nameof(ServiceBusMonitor))]
    public async Task Run(
        [ServiceBusTrigger("mapsandweatherlogging", Connection = "AzureServiceBusConnection")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        Log? log = message.Body.ToObjectFromJson<Log>(options); 
        if (log is not null)
        {
            await context.Logs.AddAsync(log);
            await context.SaveChangesAsync();
        }

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}