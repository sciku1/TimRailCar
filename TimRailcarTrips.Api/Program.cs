using TimRailcarTrips.Domain.Interfaces;
using TimRailcarTrips.Domain.Interfaces.Services;
using TimRailcarTrips.Domain.Services;
using TimRailcarTrips.Infrastructure;
using TimRailcarTrips.Infrastructure.Persistence;
using TimRailcarTrips.Infrastructure.Persistence.Seeding;
using TimRailcarTrips.Infrastructure.Repositories;

namespace TimRailcarTrips;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddInfrastructure(builder.Configuration);
        
        builder.Services.AddProblemDetails();
        
        // Repositories
        builder.Services.AddScoped<CityRepository>();
        builder.Services.AddScoped<EventCodeDefinitionRepository>();
        builder.Services.AddScoped<TripRepository>();
        builder.Services.AddScoped<TripEventRepository>();
        
        builder.Services.AddScoped<ICityRepository, CityRepository>();
        builder.Services.AddScoped<IEventCodeDefinitionRepository, EventCodeDefinitionRepository>();
        builder.Services.AddScoped<ITripRepository, TripRepository>();
        builder.Services.AddScoped<ITripEventRepository, TripEventRepository>();
        
        // Services
        builder.Services.AddScoped<ITripDomainService, TripDomainService>();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        
        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

        }

        // app.UseHttpsRedirection();


        app.UseCors("AllowAll");
        app.UseAuthorization();

        app.MapControllers();
        
        
        
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.EnsureCreatedAsync();

        var citySeeder = scope.ServiceProvider.GetRequiredService<CitySeeder>();
        await citySeeder.SeedAsync();

        var eventSeeder = scope.ServiceProvider.GetRequiredService<EventCodeDefinitionSeeder>();
        await eventSeeder.SeedAsync();

        await app.RunAsync();
    }
}