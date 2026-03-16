namespace TimRailcarTrips.DTO.Trips;

public record TripDto
{
    public string Id { get; init; } = string.Empty;
    public string EquipmentCode { get; init; } = string.Empty;
    public string Origin { get; init; } = string.Empty;
    public string Destination { get; init; } = string.Empty;
    public DateTime StartDateTime { get; init; }
    public DateTime EndDateTime { get; init; }
    public string TotalTripHours { get; init; } = string.Empty;
};