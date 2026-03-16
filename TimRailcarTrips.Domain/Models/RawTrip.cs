namespace TimRailcarTrips.Domain.Models;

public record RawTrip
{
    public required string EquipmentCode { get; init; }
    public required string EventCode { get; init; }
    public required DateTime EventTime { get; init; }
    public required int CityId { get; init; }
    
    
}