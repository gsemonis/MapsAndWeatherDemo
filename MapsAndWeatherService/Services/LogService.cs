using Azure.Messaging.ServiceBus;
using MapsAndWeatherData.Models;
using MapsAndWeatherRepository.Interfaces;
using MapsAndWeatherService.DTOs;
using MapsAndWeatherService.Interfaces;
using System.Runtime.CompilerServices;
using System.Text.Json;
namespace MapsAndWeatherService.Services
{
    public class LogService(ILogRepository logRepository, ServiceBusSender serviceBusSender) : ILogService
    {
        private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

        public async Task EnqueueLogAsync(LogDTO logDTO, CancellationToken cancellationToken = default)
        {
            string body = JsonSerializer.Serialize(logDTO, _jsonOptions);
            ServiceBusMessage message = new(body) { ContentType = @"application/json" };
            await serviceBusSender.SendMessageAsync(message, cancellationToken);
        }

        public async Task<LogDTO?> GetLogAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Log? source = await logRepository.GetLogByIdAsync(id, cancellationToken);
            if (source is null)
            {
                return null;
            }
            else return new LogDTO()
            {
                Id = source.Id,
                ClientIP = source.ClientIP,
                Path = source.Path,
                QueryString = source.QueryString,
                StatusCode = source.StatusCode,
                TimeStampUTC = source.TimeStampUTC
            };
            
        }

        public async IAsyncEnumerable<LogDTO> GetAllLogsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (Log log in logRepository.GetAllLogsAsync(cancellationToken).WithCancellation(cancellationToken))
            {
                yield return new LogDTO
                {
                    Id = log.Id,
                    ClientIP = log.ClientIP,
                    Path = log.Path,
                    QueryString = log.QueryString,
                    StatusCode = log.StatusCode,
                    TimeStampUTC = log.TimeStampUTC
                };
            }
        }
    }
}
