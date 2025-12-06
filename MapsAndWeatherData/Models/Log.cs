namespace MapsAndWeatherData.Models
{
    public class Log
    {
        public Guid Id { get; set; }
        public string? ClientIP { get; set; }
        public string? Path { get; set; }
        public string? QueryString { get; set; }
        public int? StatusCode { get; set; }
        public DateTimeOffset TimeStampUTC { get; set; }
    }
}
