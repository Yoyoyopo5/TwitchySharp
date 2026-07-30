namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelRaid"/>.
/// </summary>
/// <remarks>
/// One of <see cref="FromBroadcasterId"/> or <see cref="ToBroadcasterId"/> will be populated,
/// depending on how the subscription was created.
/// </remarks>
public record ChannelRaidCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get outgoing Raid notifications for.
    /// </summary>
    public UserId? FromBroadcasterId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) to get incoming Raid notifications for.
    /// </summary>
    public UserId? ToBroadcasterId { get; init; }
}
