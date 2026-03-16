using Microsoft.AspNetCore.Components;
using TimRailcarTrips.Client.ApiClients;
using TimRailcarTrips.Client.ApiClients.Models;

namespace TimRailcarTrips.Client.Components;

public partial class EventsTable : ComponentBase
{
    
    [Parameter]
    public string Id { get; set; } = string.Empty;
    [Inject] private TripsApiClient TripsApiClient { get; set; } = null!;
    
    public TripDetailsDto? TripDetailsDto { get; set; } = null!;
    public IList<TripEventDto>? TripEventsDto { get; set; } = null!;
    
    
    protected override async Task OnInitializedAsync()
    {
        TripDetailsDto = await TripsApiClient.Api.Trips[Id].GetAsync();
        TripEventsDto = TripDetailsDto!.TripEvents;

    }
}