using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using TimRailcarTrips.Infrastructure.Import;

namespace TimRailcarTrips.Infrastructure.Persistence.Seeding;

public abstract class BaseSeeder
{

    protected StreamReader CreateSeededDataStream(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var stream = assembly.GetManifestResourceStream($"TimRailcarTrips.Infrastructure.Persistence.Seeding.Data.{resourceName}")
                           ?? throw new InvalidOperationException($"Resource '{resourceName}' not found.");
        
        return new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
        
    } 
}