using TimRailcarTrips.Domain.Entities;
using TimRailcarTrips.Domain.Interfaces;
using TimRailcarTrips.Infrastructure.Persistence;

namespace TimRailcarTrips.Infrastructure.Repositories;

public class CityRepository(AppDbContext db) : BaseRepository<City>(db), ICityRepository;