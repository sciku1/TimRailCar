using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using TimRailcarTrips.Domain.Entities;

namespace TimRailcarTrips.Infrastructure.Persistence;

[DbContext(typeof(AppDbContext))]
[Migration("Persistence/Migrations")]
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<EventCodeDefinition> EventCodeDefinitions => Set<EventCodeDefinition>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Trip>(t =>
        {
            t.HasKey(x => x.Id);
            t.Property(x => x.StartDate);
            t.Property(x => x.EndDate);
            t.Property(x => x.EquipmentCode);
            t.Property(x => x.TotalTripHours);
            t.Property(x => x.OriginCityId);
            t.Property(x => x.DestinationCityId); 
            
            t.HasOne(x => x.OriginCity)
                .WithMany(c => c.OriginTrips)
                .HasForeignKey(x => x.OriginCityId);

            t.HasOne(x => x.DestinationCity)
                .WithMany(c => c.DestinationTrips)
                .HasForeignKey(x => x.DestinationCityId);
            
        });
        
        builder.Entity<City>(c =>
        {
            c.HasKey(x => x.Id);
            c.Property(x => x.CityName).IsRequired().HasMaxLength(200);
            c.Property(x => x.TimeZone).IsRequired().HasMaxLength(100);
        });

        builder.Entity<EventCodeDefinition>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd(); 

            e.Property(x => x.Code).IsRequired().HasMaxLength(1);
            e.HasIndex(x => x.Code);
            
            e.Property(x => x.DescriptionShort).IsRequired().HasMaxLength(200);
            e.Property(x => x.DescriptionLong).HasMaxLength(2000);
        });
        
        builder.Entity<TripEvent>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.EventDateTime)
                .IsRequired();
            
            entity.HasIndex(e => e.TripId);

            entity.HasOne(e => e.EventCodeDefinition)
                .WithMany()
                .HasForeignKey(e => e.EventCodeId);
            
            entity.HasOne(e => e.Trip)
                .WithMany(t => t.TripEvents)
                .HasForeignKey(e => e.TripId);

            entity.HasOne(e => e.City)
                .WithMany()
                .HasForeignKey(e => e.CityId);

        });

    }
}