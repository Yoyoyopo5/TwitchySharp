using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelRaid"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelraid">Channel Raid</see> for more information.
/// </remarks>
public record ChannelRaidNotification : EventSubNotification<ChannelRaidEvent, ChannelRaidCondition>;
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
    public string? FromBroadcasterId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) to get incoming Raid notifications for.
    /// </summary>
    public string? ToBroadcasterId { get; init; }
}
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelRaid"/> event.
/// </summary>
public record ChannelRaidEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that created the raid.
    /// </summary>
    public required string FromBroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that created the raid.
    /// </summary>
    public required string FromBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that created the raid.
    /// </summary>
    public required string FromBroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that received the raid.
    /// </summary>
    public required string ToBroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that received the raid.
    /// </summary>
    public required string ToBroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that received the raid.
    /// </summary>
    public required string ToBroadcasterUserName { get; init; }
    /// <summary>
    /// The number of viewers in the raid.
    /// </summary>
    public required int Viewers { get; init; }
}
