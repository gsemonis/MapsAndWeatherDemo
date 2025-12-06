namespace MapsAndWeatherService.DTOs
{
    public record LogDTO
    {
        public Guid Id { get; init; }
        public string? ClientIP { get; init; }
        public string? Path { get; init; }
        public string? QueryString { get; init; }
        public int? StatusCode { get; init; }
        public DateTimeOffset TimeStampUTC { get; init; }
    }
}
