namespace TimRailcarTrips.Domain.Entities;

public class Trip
{
    public int Id { get; init; }
    
    public required string EquipmentCode { get; init; }
    public int OriginCityId { get; init; }
    public int DestinationCityId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    
    public double TotalTripHours { get; init; }
    
    
    public ICollection<TripEvent> TripEvents { get; init; } = new List<TripEvent>();
    public City OriginCity { get; init; } = null!;
    public City DestinationCity { get; init; } = null!;


}
