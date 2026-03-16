using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimRailcarTrips.Infrastructure.Import;
using TimRailcarTrips.Infrastructure.Persistence;
using TimRailcarTrips.Infrastructure.Persistence.Seeding;

namespace TimRailcarTrips.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration config)
    {
        // Register DBContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection")));
        
        // Register data seeders
        // TODO: We could do something fancy with Reflection, but overkill for only 2 seeders
        services.AddScoped<CsvParser>();
        services.AddScoped<CitySeeder>();
        services.AddScoped<EventCodeDefinitionSeeder>();
        
        return services;
    }
    
    
}
