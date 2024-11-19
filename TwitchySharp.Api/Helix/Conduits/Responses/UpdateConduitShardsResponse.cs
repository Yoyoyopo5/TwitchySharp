using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Contains a list of updated shards and shards that failed to update.
/// </summary>
public record UpdateConduitShardsResponse
{
    /// <summary>
    /// List of successfully updated shards.
    /// </summary>
    public required ConduitShard[] Data { get; init; }
    /// <summary>
    /// List of unsuccessful updates.
    /// </summary>
    public required ConduitUpdateError[] Errors { get; init; }
}
