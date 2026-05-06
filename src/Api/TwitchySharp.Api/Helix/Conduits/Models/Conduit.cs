using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Conduits;

/// <summary>
/// Contains data about a specific conduit.
/// </summary>
public record Conduit
{
    /// <summary>
    /// The id of the conduit.
    /// </summary>
    public required ConduitId Id { get; init; }
    /// <summary>
    /// The number of shards associated with this conduit.
    /// </summary>
    public required int ShardCount { get; init; }
}
