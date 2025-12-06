using MapsAndWeatherService.DTOs;
using MapsAndWeatherService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MapsAndWeatherMVC.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController(ILogService logService) : ControllerBase
    {
        // GET: api/<LogsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogDTO>>> Get()
        {
            List<LogDTO> logs = new();

            await foreach (var dto in logService.GetAllLogsAsync())
            {
                logs.Add(dto);
            }
            return Ok(logs);
        }

        // GET api/<LogsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogDTO>> Get(Guid id)
        {
            LogDTO? log = await logService.GetLogAsync(id);
            if (log is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(log);
            }
        }

      
    }
}
