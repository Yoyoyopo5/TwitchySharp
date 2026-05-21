namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ConduitShardDisabled"/> event.
/// </summary>
public record ConduitShardDisabledEvent
{
    /// <summary>
    /// The id of the conduit that had a shard disabled.
    /// </summary>
    public required ConduitId ConduitId { get; init; }
    /// <summary>
    /// The id of the shard that was disabled.
    /// </summary>
    public required ConduitShardId ShardId { get; init; }
    /// <summary>
    /// The new status of the transport.
    /// </summary>
    public required ConduitShardStatus Status { get; init; }
    /// <summary>
    /// The disabled transport.
    /// </summary>
    public required ConduitTransport Transport { get; init; }
}
