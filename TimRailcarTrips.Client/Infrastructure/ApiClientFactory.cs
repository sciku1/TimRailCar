using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using TimRailcarTrips.Client.ApiClients;

namespace TimRailcarTrips.Client.Infrastructure;

public class TripsApiClientFactory
{
    
    private readonly HttpClient _httpClient;

    public TripsApiClientFactory(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public TripsApiClient GetClient() {
        return new TripsApiClient(new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: _httpClient ));
    }
}