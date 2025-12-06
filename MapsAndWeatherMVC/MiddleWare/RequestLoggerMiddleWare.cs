using MapsAndWeatherService.DTOs;
using MapsAndWeatherService.Interfaces;

namespace MapsAndWeatherMVC.MiddleWare
{
    public class RequestLoggerMiddleWare(RequestDelegate next, ILogService logService)
    {

        public async Task InvokeAsync(HttpContext context)
        {
            string? clientIp = context.Connection.RemoteIpAddress?.ToString();
            string path = context.Request.Path;
            string? queryString = context.Request.QueryString.HasValue ? context.Request.QueryString.Value! : null;
            int? statusCode = null;
            try
            {
                await next(context);
                statusCode = context.Response?.StatusCode;
            }
            catch( Exception)
            {
                statusCode = 500;
                throw;
            }
            finally
            {
                LogDTO log = new()
                {
                    ClientIP = clientIp,
                    Path = path,
                    QueryString = queryString,
                    StatusCode = statusCode
                };
                try
                {
                    await logService.EnqueueLogAsync(log);
                }
                catch (Exception) { }
            }
        }
    }
}
