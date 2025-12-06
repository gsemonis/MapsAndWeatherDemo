using MapsAndWeatherData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapsAndWeatherData.Configuration
{
    internal class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Settings");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(s => s.Key).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Value).IsRequired().HasMaxLength(100);
            builder.HasIndex(s => s.Key).IsUnique().IncludeProperties(s => s.Value);
        }         
    }
}
