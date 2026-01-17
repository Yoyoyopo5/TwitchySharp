using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Models.Helix.Conduits.Models;

/// <summary>
/// Contains information about a specific conduit shard.
/// </summary>
public record ConduitShard
{
    /// <summary>
    /// The id of the shard.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The shard status.
    /// The subscriber receives events only for <see cref="ConduitShardStatus.Enabled"/> shards. 
    /// </summary>
    public required ConduitShardStatus Status { get; init; }
    /// <summary>
    /// The transport details used to send the notifications.
    /// </summary>
    public required ConduitTransport Transport { get; init; }
}
