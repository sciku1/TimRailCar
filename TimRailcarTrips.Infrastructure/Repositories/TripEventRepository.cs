using TimRailcarTrips.Domain.Entities;
using TimRailcarTrips.Domain.Interfaces;
using TimRailcarTrips.Infrastructure.Persistence;

namespace TimRailcarTrips.Infrastructure.Repositories;

public class TripEventRepository(AppDbContext db) : BaseRepository<TripEvent>(db), ITripEventRepository;