using TimRailcarTrips.Domain.Entities;
using TimRailcarTrips.Infrastructure.Import;

namespace TimRailcarTrips.Infrastructure.Persistence.Seeding;

public class EventCodeDefinitionSeeder(AppDbContext db, CsvParser csvParser): BaseSeeder
{
    public async Task SeedAsync(CancellationToken ct = default)
    {

        using var stream = CreateSeededDataStream("event_code_definitions.csv");
        
        await foreach (var parts in csvParser.ParseAsyncEnumerable(stream, ct))
        {
            // Validation could be much better
            var code             = parts[0].Trim();
            var descriptionShort = parts[1].Trim();
            var descriptionLong  = parts[2].Trim();

            var definition = new EventCodeDefinition
            {
                Code = code,
                DescriptionShort = descriptionShort,
                DescriptionLong = descriptionLong,
            };
            
            await db.EventCodeDefinitions.AddAsync(definition, ct);
        }

        await db.SaveChangesAsync(ct);
    }
}