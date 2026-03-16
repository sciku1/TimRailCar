using Microsoft.AspNetCore.Components;
using TimRailcarTrips.Client.ApiClients;
using TimRailcarTrips.Client.ApiClients.Models;

namespace TimRailcarTrips.Client.Components;

public partial class TripTable : ComponentBase
{
    
    [Inject] private TripsApiClient TripsApiClient { get; set; } = null!;
    
    protected IList<TripDto>? Trips { get; set; } = new List<TripDto>();
    
    protected override async Task OnInitializedAsync()
    {
        Trips = await TripsApiClient.Api.Trips.GetAsync();
        
    }
}