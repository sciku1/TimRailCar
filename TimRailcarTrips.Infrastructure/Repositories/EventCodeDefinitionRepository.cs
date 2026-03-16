using TimRailcarTrips.Domain.Entities;
using TimRailcarTrips.Domain.Interfaces;
using TimRailcarTrips.Infrastructure.Persistence;

namespace TimRailcarTrips.Infrastructure.Repositories;

public class EventCodeDefinitionRepository(AppDbContext db) : BaseRepository<EventCodeDefinition>(db), IEventCodeDefinitionRepository;