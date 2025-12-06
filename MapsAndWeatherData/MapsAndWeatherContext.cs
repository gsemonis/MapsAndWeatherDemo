using MapsAndWeatherData.Models;
using Microsoft.EntityFrameworkCore;

namespace MapsAndWeatherData
{
    public class MapsAndWeatherContext(DbContextOptions<MapsAndWeatherContext> options) : DbContext(options)
    {       
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MapsAndWeatherContext).Assembly);
        }
    }
}
