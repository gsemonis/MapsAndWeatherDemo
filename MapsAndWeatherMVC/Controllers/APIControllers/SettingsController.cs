using MapsAndWeatherService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MapsAndWeatherMVC.Controllers.APIControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController(ISettingService settingService) : ControllerBase
    {
        [HttpGet("{settingName}")]
        public async Task<IActionResult> GetSettingAsync(string settingName)
        {
            string? settingValue = await settingService.GetSettingValueAsync(settingName);
            if (string.IsNullOrWhiteSpace(settingValue))
            {
                return NotFound(new { message = $"Setting '{settingName}' not found." });
            }
            return Ok(new { value = settingValue });
        }
    }
}
