using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapsAndWeatherRepository.Interfaces
{
    public interface ISettingRepository
    {
        Task<string?> GetSettingValueAsync(string key, CancellationToken cancellationToken = default);
        Task UpsertSettingAsync(string key, string value, CancellationToken cancellationToken = default);
    }
}
