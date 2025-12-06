using MapsAndWeatherService.DTOs;

namespace MapsAndWeatherService.Interfaces
{
    public interface ILogService
    {
        Task EnqueueLogAsync(LogDTO logDTO, CancellationToken cancellationToken = default);
        IAsyncEnumerable<LogDTO> GetAllLogsAsync(CancellationToken cancellationToken = default);
        Task<LogDTO?> GetLogAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
