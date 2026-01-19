namespace TwitchySharp.Api.Helix.Conduits;

/// <summary>
/// Contains information used to update a specific shard.
/// </summary>
public record ConduitShardUpdate
{
    /// <summary>
    /// The id of the shard to update.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The transport details that you want to update the shard to.
    /// Use derived classes <see cref="ConduitWebsocketTransportUpdate"/> and <see cref="ConduitWebhookTransportUpdate"/>.
    /// </summary>
    public required ConduitTransportUpdate Transport { get; set; }
}
