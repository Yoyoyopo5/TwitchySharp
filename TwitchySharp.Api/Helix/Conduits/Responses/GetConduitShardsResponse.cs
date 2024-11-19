using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Contains information about a requested conduit's shards.
/// </summary>
public record GetConduitShardsResponse
{
    /// <summary>
    /// List of information about a conduit's shards.
    /// </summary>
    public required ConduitShard[] Data { get; init; }
    /// <summary>
    /// Contains information used to page through a list of results. 
    /// The <see cref="Pagination.Cursor"/> is <see langword="null"/> if there are no more pages left to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
}
