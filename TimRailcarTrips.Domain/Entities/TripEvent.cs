namespace TimRailcarTrips.Domain.Entities;

public class TripEvent
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public int EventCodeId { get; set; }
    public DateTime EventDateTime { get; set; }
    public int CityId { get; set; }

    
    public Trip Trip { get; set; } = null!;
    public City City { get; set; } = null!;
    
    public EventCodeDefinition EventCodeDefinition { get; init; } = null!;
}