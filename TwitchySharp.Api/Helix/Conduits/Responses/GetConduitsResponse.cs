using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Contains a list of conduits.
/// </summary>
public record GetConduitsResponse
{
    /// <summary>
    /// The list of conduits.
    /// </summary>
    public required Conduit[] Data { get; init; }
}

/// <summary>
/// Contains data about a specific conduit.
/// </summary>
public record Conduit
{
    /// <summary>
    /// The id of the conduit.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The number of shards associated with this conduit.
    /// </summary>
    public required int ShardCount { get; init; }
}
