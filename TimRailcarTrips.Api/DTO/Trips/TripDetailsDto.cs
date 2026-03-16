namespace TimRailcarTrips.DTO.Trips;

public record TripDetailsDto : TripDto
{

    public List<TripEventDto>? TripEvents { get; init; }
};