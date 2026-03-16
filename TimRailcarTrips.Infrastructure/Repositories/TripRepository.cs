using Microsoft.EntityFrameworkCore;
using TimRailcarTrips.Domain.Entities;
using TimRailcarTrips.Domain.Interfaces;
using TimRailcarTrips.Infrastructure.Persistence;

namespace TimRailcarTrips.Infrastructure.Repositories;

public class TripRepository(AppDbContext db) : BaseRepository<Trip>(db), ITripRepository
{


    public override async Task<IReadOnlyList<Trip>> GetAllAsync(CancellationToken ct = default)
        => await db.Set<Trip>()
            .Include(o => o.OriginCity)
            .Include(o => o.DestinationCity).ToListAsync(ct);

    public override async Task<Trip> GetByIdAsync(int id, CancellationToken ct = default)
    {

        return await db.Set<Trip>().Include(o => o.OriginCity)
            .Include(o => o.DestinationCity)
            .Include(o => o.TripEvents)
            .ThenInclude(o=> o.City)
            .Include(o => o.TripEvents)
            .ThenInclude(o => o.EventCodeDefinition)
            .FirstAsync(o => o.Id== id, ct);
    
    }
}