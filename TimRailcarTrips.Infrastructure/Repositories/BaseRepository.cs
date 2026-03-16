using Microsoft.EntityFrameworkCore;
using TimRailcarTrips.Infrastructure.Persistence;

namespace TimRailcarTrips.Infrastructure.Repositories;

public abstract class BaseRepository<T>(AppDbContext db) where T : class
{
    
    protected virtual IQueryable<T> AddIncludes(IQueryable<T> query) => query;

    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await db.Set<T>().FindAsync([id], ct);
    }

    public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
        => await db.Set<T>().ToListAsync(ct);

    public virtual async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await db.Set<T>().AddAsync(entity, ct);
        await db.SaveChangesAsync(ct);
    }
    
    public virtual async Task AddManyAsync(IEnumerable<T> entities, CancellationToken ct = default)
    {
        db.Set<T>().AddRange(entities);
        await db.SaveChangesAsync(ct);
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        db.Set<T>().Update(entity);
        await db.SaveChangesAsync(ct);
    }

    public virtual async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await GetByIdAsync(id, ct);
        if (entity is not null)
        {
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync(ct);
        }
    }
    
    
}