using TimRailcarTrips.Domain.Constants;
using TimRailcarTrips.Domain.Entities;
using TimRailcarTrips.Domain.Interfaces;
using TimRailcarTrips.Domain.Interfaces.Services;
using TimRailcarTrips.Domain.Models;

namespace TimRailcarTrips.Domain.Services;

public class TripDomainService(ITripRepository tripRepository, IEventCodeDefinitionRepository eventCodeDefinitionRepository, ICityRepository cityRepository) : ITripDomainService
{
    
    public async Task ImportTripsAsync(IAsyncEnumerable<string[]> rawTrips)
    {
        
        // TODO: Cutting time short, there's a few things I won't have time to implement
        // 1. Validation -- does the city ID exist?
        // 2. Row validation -- Does the row have proper data?
        
        
        // CSV File
        // 0 - Equipment "Code"
        // 1 - Event Code
        // 2 - Event Time
        // 3 - City Id

        var citiesIdToCity = (await cityRepository.GetAllAsync()).ToDictionary(c => c.Id);;
        
        var equipmentGroupedTrips = await rawTrips.Select(o =>
        {
            var cityId = int.Parse(o[3]);
            var city = citiesIdToCity[cityId];
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(city.TimeZone);
            var unspecifiedDateTime = DateTime.SpecifyKind(DateTime.Parse(o[2]), DateTimeKind.Unspecified);

            
            // TODO: There's probably a better way to deal with this edge case, however this is a bit more predictable. 
            // I think there's probably a better method with GetAdjustmentRules(), however doing this to save time.
            if (timezone.IsInvalidTime(unspecifiedDateTime))
            {
                unspecifiedDateTime = unspecifiedDateTime.AddHours(1);
            }
            
            return new RawTrip
            {
                EquipmentCode = o[0],
                EventCode = o[1],
                EventTime = TimeZoneInfo.ConvertTimeToUtc(unspecifiedDateTime, timezone),
                CityId = cityId,
            };
        }).OrderBy(o => o.EquipmentCode).GroupBy(o => o.EquipmentCode).Select(o => new
        {
            EquipmentCode = o.Key,
            RawTrips = o.OrderBy(r => r.EventTime).ToList(), 
        }).ToListAsync();

        IList<List<RawTrip>> eventGroupedTrips = new List<List<RawTrip>>();
        List<RawTrip> current = [];

        
        foreach (var equipmentTrips in equipmentGroupedTrips)
        {
            foreach (var rawTrip in equipmentTrips.RawTrips)
            {
                if (rawTrip.EventCode == EventCodes.Released)
                {
                    // Create a new bucket, discarding what was available
                    current = [];
                }
                
                current.Add(rawTrip);
                
                if (rawTrip.EventCode == EventCodes.Placed)
                {
                    // Add the event to the total bucket
                    eventGroupedTrips.Add(current);
                    current = [];
                }
            }

        }
        
        // Now, we can just persist the data in the database
        var eventCodes = await eventCodeDefinitionRepository.GetAllAsync();
        var trips = (from tripEventGroup in eventGroupedTrips
            let first = tripEventGroup.First()
            let last = tripEventGroup.Last()
            let totalTimeSpan = last.EventTime - first.EventTime
            select new Trip
            {
                // Doesn't really matter
                EquipmentCode = first.EquipmentCode, 
                DestinationCityId = last.CityId,
                OriginCityId = first.CityId,
                StartDate = first.EventTime,
                EndDate = last.EventTime,
                TotalTripHours = totalTimeSpan.TotalHours,
                TripEvents = tripEventGroup.Select(e => new TripEvent
                    {
                        CityId = first.CityId,
                        // TODO: Better error handling here, should this go to the repository? Very likely yes,
                        // since we'd want to just load and cache. But for a small table, this will be fine
                        EventCodeId = eventCodes.First(o => o.Code == e.EventCode).Id,
                        EventDateTime = e.EventTime,
                    })
                    .ToList()
            }).ToList();

        await tripRepository.AddManyAsync(trips);
    }
}