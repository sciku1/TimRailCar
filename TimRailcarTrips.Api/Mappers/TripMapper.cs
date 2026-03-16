using System.Globalization;
using TimRailcarTrips.Domain.Entities;
using TimRailcarTrips.DTO.Trips;

namespace TimRailcarTrips.Mappers;

public static class TripMapper
{
    public static TripDto ToModel(this Trip trip)
    {
        
        return new TripDto
        {
            Id = trip.Id.ToString(),
            EquipmentCode = trip.EquipmentCode,
            Origin = trip.OriginCity.CityName,
            Destination = trip.DestinationCity.CityName,
            StartDateTime = DateTime.SpecifyKind(trip.StartDate, DateTimeKind.Utc),
            EndDateTime = DateTime.SpecifyKind(trip.EndDate, DateTimeKind.Utc),
            TotalTripHours = Math.Round(trip.TotalTripHours, 2).ToString(CultureInfo.InvariantCulture),
        };
    }
    
    public static TripDetailsDto ToDetailModel(this Trip trip)
    {
        var model = trip.ToModel();
        return new TripDetailsDto
        {
            Id = trip.Id.ToString(),
            EquipmentCode = model.EquipmentCode,
            Origin = model.Origin,
            Destination = model.Destination,
            StartDateTime = model.StartDateTime,
            EndDateTime = model.EndDateTime,
            TotalTripHours = model.TotalTripHours,
            TripEvents = trip.TripEvents.Select(o => new TripEventDto
            {
                 EventCode = o.EventCodeDefinition.Code, 
                 EventTime = o.EventDateTime,
                 CityName  = o.City.CityName
            }).ToList()
        };
    }
}