namespace TimRailcarTrips.Domain.Entities;

public class City
{
    public int Id { get; init; }
    public required string CityName { get; init; }
    public required string TimeZone { get; init; }
    
    
    public ICollection<Trip> OriginTrips { get; init; } = new List<Trip>();
    public ICollection<Trip> DestinationTrips { get; init; } = new List<Trip>();


}