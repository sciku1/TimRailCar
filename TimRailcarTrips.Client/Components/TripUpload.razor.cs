using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Kiota.Abstractions;
using TimRailcarTrips.Client.ApiClients;
using TimRailcarTrips.Client.ApiClients.Api.Trips.Upload;

namespace TimRailcarTrips.Client.Components;

public partial class TripUpload : ComponentBase
{
    private IFileEntry? _file;
    
    
    [Inject]
    private TripsApiClient ApiClient { get; set; } = null!;

    private void OnFileSelected(FileChangedEventArgs e)
        => _file = e.Files.FirstOrDefault();

    private async Task UploadFile()
    {
        if (_file == null) return;
        
        
        using var memoryStream = new MemoryStream();
        await _file.OpenReadStream(maxAllowedSize: 10_000_000).CopyToAsync(memoryStream);
        memoryStream.Position = 0;
        
        var body = new MultipartBody();
        body.AddOrReplacePart(
            "file",
            "application/octet-stream",
            memoryStream.ToArray(),
            _file.Name
        );


        try
        {
            await ApiClient.Api.Trips.Upload.PostAsync(body);
        }
        catch
        {
            Console.WriteLine("Upload Failed");
        }
        
        
        
        

    }
}