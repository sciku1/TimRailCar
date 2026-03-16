using System.Reflection;
using System.Runtime.CompilerServices;

namespace TimRailcarTrips.Infrastructure.Import;

public class CsvParser
{
    public async IAsyncEnumerable<string[]> ParseAsyncEnumerable(StreamReader stream, [EnumeratorCancellation] CancellationToken ct = default)
    {
        
        var content = await stream.ReadToEndAsync(ct);
        
        // TODO: This is quite simple right now, however with larger file sizes,
        // there's a good likely hood this may run out of memory. The interface is 
        // Made such that it can be swapped out of a chunked approach.
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var line in lines.Skip(1)) // Skip 1 to skip header
        {
            var split = line.Split(',');
            yield return split;
        }
    } 
}