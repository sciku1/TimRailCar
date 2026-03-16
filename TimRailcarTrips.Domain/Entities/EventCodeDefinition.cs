namespace TimRailcarTrips.Domain.Entities;

public class EventCodeDefinition
{
    public int Id { get; init; }
    public required string Code { get; init; }
    public required string DescriptionShort { get; init; }
    public required string DescriptionLong { get; init; }
}