using MapsAndWeatherData;
using MapsAndWeatherData.Models;
using MapsAndWeatherRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapsAndWeatherRepository.Repositories
{
    public class LogRepository(MapsAndWeatherContext mapsAndWeatherContext) : ILogRepository
    {
        public IAsyncEnumerable<Log> GetAllLogsAsync(CancellationToken cancellationToken = default)
        {
            return mapsAndWeatherContext.Logs.AsNoTracking().OrderByDescending(log => log.TimeStampUTC).AsAsyncEnumerable();
        }

        public async Task<Log?> GetLogByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await mapsAndWeatherContext.Logs.FindAsync(id, cancellationToken);
        }
    }
}
