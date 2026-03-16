## To Run
```
dotnet run --project TimRailcarTrips.Api
dotnet run --project TimRailcarTrips.Client
```


## Code Architecture
The project largely follows a Clean Architecture + DDD "Lite" setup.

# Considerations

## Database choice
SQLite as a driver was chosen strictly as a "flexibility" selection. The usage patterns 
of this application are unknown. 

## Online vs Offline 
An application like this could be hosted online or offline using SQLite as a data backing.
An online always solution was chosen due to the nature of the data -- it seems like there needs 
to be a central source of truth, the trips timezone.
Because the domain service is largely agnostic to the data layer, the path to moving the logic
offline is relatively straightforward and has multiple potential paths, example: 
1. TimRailcarTrips.Infrastructure > TimRailcarTrips.Api becomes TimRailcarTrips.Infrastructure > TimRailcarTrips.Client, and the Blazor ClientTripService can leverage the TripDomainService to perform logic clientside. 
2. A new TimRailcarTrips.Client.Infrastructure could be created, and the IClientTripService can have multiple implementations (one local, one remote, if desired).

## Timezone handling
There wasn't much in the requirements around handling timezone edge cases. The path I took was to simply add an hour for any
timezones times that were invalid and move on. It also assumes that all the timezones are standardized timezones 
(although better error handling could be made). A better approach could be to use NodaTime, however 

## Invalid trips
Trips are considered invalid if they do not have an Origin and Destination (W > Z), and are discarded / not included 

## UI/UX
I levereaged Blazor.DataGrid to allow for better styling. Generally, a table component could have worked, however 
Blazorise makes it easier, and generally accounts for a bunch of edge cases. Currently, the Datatables load everything
into memory. With more time, I would instead pass that to the backend and only retrieve the currently viewable results.

## Tests
Tests were omitted, however could be added with relative ease (TimRailcarTrips.Tests.Unit, TimRailcarTrips.Tests.Integration, etc.)
Should be relatively easily testable because repository can be swapped out with a mock.