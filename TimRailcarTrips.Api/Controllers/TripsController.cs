using System.Text;
using Microsoft.AspNetCore.Mvc;
using TimRailcarTrips.Domain.Interfaces;
using TimRailcarTrips.Domain.Interfaces.Services;
using TimRailcarTrips.DTO.Trips;
using TimRailcarTrips.Infrastructure.Import;
using TimRailcarTrips.Mappers;

namespace TimRailcarTrips.Controllers;

[ApiController]
public class TripsController(CsvParser csvParser, ITripDomainService tripDomainService, ITripRepository tripRepository) : ControllerBase
{
    
    [HttpPost("api/trips/upload")]
    public async Task<IActionResult> UploadAsync(IFormFile file)
    {
        // TODO: IFormFile uploads the entire thing into memory. With how the data is setup, you could 
        // get significant advantages from the parsing reading each row incrementally.
        var stream = file.OpenReadStream();

        using var readStream = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);

        var enumerable = csvParser.ParseAsyncEnumerable(readStream);

        await tripDomainService.ImportTripsAsync(enumerable);
        

        return Ok(new {  name = file.FileName });
    }
    
    [HttpGet("api/trips")]
    public async Task<ActionResult<TripDto[]>> GetAsync()
    {
        var trips = await tripRepository.GetAllAsync();
        return Ok(trips.Select(t => t.ToModel()).ToArray());
    }
    
    
    [HttpGet("api/trips/{id}")]
    public async Task<ActionResult<TripDetailsDto>> GetAsync([FromRoute] int id)
    {
        var trip = await tripRepository.GetByIdAsync(id);
        if (trip == null)
        {
            return NotFound();
        }
        return Ok(trip.ToDetailModel());
    }
}