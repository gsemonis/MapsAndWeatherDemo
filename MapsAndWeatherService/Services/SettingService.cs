using MapsAndWeatherRepository.Interfaces;
using MapsAndWeatherRepository.Repositories;
using MapsAndWeatherService.Interfaces;

namespace MapsAndWeatherService.Services
{
    public class SettingService(ISettingRepository settingRepository) : ISettingService
    {
        public async Task<string?> GetSettingValueAsync(string key, CancellationToken cancellationToken = default)
        {
            return await settingRepository.GetSettingValueAsync(key, cancellationToken);
        }
    }
}
