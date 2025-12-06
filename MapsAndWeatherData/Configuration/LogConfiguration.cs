using MapsAndWeatherData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapsAndWeatherData.Configuration
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(x => x.TimeStampUTC).ValueGeneratedOnAdd().HasDefaultValueSql("SYSDATETIMEOFFSET()").IsRequired();
            builder.HasIndex(s => s.TimeStampUTC);
        }
    }
}
