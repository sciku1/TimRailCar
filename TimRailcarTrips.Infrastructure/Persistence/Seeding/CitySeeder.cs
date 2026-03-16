using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TimRailcarTrips.Domain.Entities;
using TimRailcarTrips.Infrastructure.Import;

namespace TimRailcarTrips.Infrastructure.Persistence.Seeding;

public class CitySeeder(AppDbContext db, CsvParser csvParser) : BaseSeeder
{
    public async Task SeedAsync(CancellationToken ct = default)
    {
        if (await db.Cities.AnyAsync(ct))
        {
            Console.WriteLine("Cities already seeded, skipping.");
            return;
        }

        using var stream = CreateSeededDataStream("canadian_cities.csv");
        
        await foreach (var parts in csvParser.ParseAsyncEnumerable(stream, ct))
        {
            // Todo: Validation could be better here, but the list is known ahead of time.
            var id       = int.Parse(parts[0].Trim());
            var cityName = parts[1].Trim();
            var timeZone = parts[2].Trim();
            

            var city = new City
            {
                Id = id,
                CityName = cityName,
                TimeZone = timeZone,
            };
            
            await db.Cities.AddAsync(city, ct);
        }

        await db.SaveChangesAsync(ct);
    }
}