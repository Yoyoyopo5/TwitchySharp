namespace TwitchySharp.Api.Helix.Conduits.Models;

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
