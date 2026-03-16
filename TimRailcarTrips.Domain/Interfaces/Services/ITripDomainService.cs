namespace TimRailcarTrips.Domain.Interfaces.Services;

public interface ITripDomainService
{
    public Task ImportTripsAsync(IAsyncEnumerable<string[]> rawTrips);
}