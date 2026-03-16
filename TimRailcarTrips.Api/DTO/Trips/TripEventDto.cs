namespace TimRailcarTrips.DTO.Trips;

public record TripEventDto
{
    public string EventCode { get; init; } = string.Empty;
    public DateTime EventTime { get; init; }
    public string CityName { get; init; } = string.Empty;
}