using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Models.Notifications.Channel;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelFollow"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelfollow">Channel Follow</see> for more information.
/// </remarks>
public record ChannelFollowNotification : EventSubNotification<ChannelFollowEvent, ChannelFollowCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelFollow"/>.
/// </summary>
public record ChannelFollowCondition : BroadcasterModeratorCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelFollow"/> event.
/// </summary>
public record ChannelFollowEvent : IHaveBroadcaster, IHaveUser
{
    /// <summary>
    /// The id of the user that followed the channel.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that followed the channel.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that followed the channel.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that was followed.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that was followed.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that was followed.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The date and time when the follow occurred.
    /// </summary>
    public required DateTimeOffset FollowedAt { get; init; }
}
