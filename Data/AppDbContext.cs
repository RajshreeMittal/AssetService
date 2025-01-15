using AssetService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AssetService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set precision and scale for the decimal property 'Value'
            modelBuilder.Entity<Asset>()
                .Property(a => a.Value)
                .HasColumnType("decimal(18,2)"); // Precision: 18, Scale: 2

            // Optionally, you can also use HasPrecision
            modelBuilder.Entity<Asset>()
                .Property(a => a.Value)
                .HasPrecision(18, 2);

        }
    }



}
