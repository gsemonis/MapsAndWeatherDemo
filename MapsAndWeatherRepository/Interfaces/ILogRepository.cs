using MapsAndWeatherData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapsAndWeatherRepository.Interfaces
{
    public interface ILogRepository
    {
        IAsyncEnumerable<Log> GetAllLogsAsync(CancellationToken cancellationToken);
        Task<Log?> GetLogByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
