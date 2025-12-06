namespace MapsAndWeatherService.Interfaces
{
    public interface ISettingService
    {
        Task<string?> GetSettingValueAsync(string key, CancellationToken cancellationToken = default);
    }
}