using MapsAndWeatherData;
using MapsAndWeatherData.Models;
using MapsAndWeatherRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MapsAndWeatherRepository.Repositories
{
    public class SettingRepository(MapsAndWeatherContext mapsAndWeatherContext, IMemoryCache cache) : ISettingRepository
    {
        public async Task<string?> GetSettingValueAsync(string key, CancellationToken cancellationToken = default)
        {
            return await cache.GetOrCreateAsync(key, async entry =>
            {                
                string? t = await mapsAndWeatherContext.Settings
                    .AsNoTracking()
                    .Where(s => s.Key == key)
                    .Select(s => s.Value)
                    .FirstOrDefaultAsync(cancellationToken);
                entry.AbsoluteExpirationRelativeToNow = string.IsNullOrWhiteSpace(t) ? TimeSpan.FromSeconds(1) : TimeSpan.FromMinutes(5);
                return t;
            });
        }

        public async Task UpsertSettingAsync(string key, string value, CancellationToken cancellationToken = default)
        {
            Setting? setting = await mapsAndWeatherContext.Settings.FirstOrDefaultAsync(f => f.Key == key, cancellationToken);
            if (setting is null)
            {
                setting = new Setting
                {
                    Key = key,
                    Value = value
                };
                mapsAndWeatherContext.Settings.Add(setting);
            }
            else
            {
                setting.Value = value;
            }
            await mapsAndWeatherContext.SaveChangesAsync(cancellationToken);
        }
    }
}
