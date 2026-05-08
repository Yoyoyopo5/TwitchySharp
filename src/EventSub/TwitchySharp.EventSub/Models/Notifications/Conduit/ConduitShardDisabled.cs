using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Models.Notifications.Conduit;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ConduitShardDisabled"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#conduitsharddisabled">Conduit Shard Disabled</see> for more information.
/// </remarks>
public record ConduitShardDisabledNotification : EventSubNotification<ConduitShardDisabledEvent, ConduitShardDisabledCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ConduitShardDisabled"/>.
/// </summary>
public record ConduitShardDisabledCondition : ClientCondition
{
    /// <summary>
    /// The id of the conduit the notification is for.
    /// If <see langword="null"/>, events for all of the client's conduits are sent.
    /// </summary>
    public string? ConduitId { get; init; }
}
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ConduitShardDisabled"/> event.
/// </summary>
public record ConduitShardDisabledEvent
{
    /// <summary>
    /// The id of the conduit that had a shard disabled.
    /// </summary>
    public required string ConduitId { get; init; }
    /// <summary>
    /// The id of the shard that was disabled.
    /// </summary>
    public required string ShardId { get; init; }
    /// <summary>
    /// The new status of the transport.
    /// </summary>
    public required ConduitShardStatus Status { get; init; }
    /// <summary>
    /// The disabled transport.
    /// </summary>
    public required ConduitTransport Transport { get; init; }
}
