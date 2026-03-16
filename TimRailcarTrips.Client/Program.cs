using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TimRailcarTrips.Client;
using TimRailcarTrips.Client.ApiClients;
using TimRailcarTrips.Client.Infrastructure;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();


var apiBaseUrl = builder.Configuration["ApiBaseUrl"]!;


// Kiota Setup
builder.Services.AddKiotaHandlers();

builder.Services.AddHttpClient<TripsApiClientFactory>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
}).AttachKiotaHandlers();
// Register the GitHub client
builder.Services.AddScoped(sp => sp.GetRequiredService<TripsApiClientFactory>().GetClient());



await builder.Build().RunAsync();
