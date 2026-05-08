using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.Vip;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelVIPRemove"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelvipremove">Channel VIP Remove</see> for more information.
/// </remarks>
public record ChannelVipRemoveNotification : EventSubNotification<ChannelVipRemoveEvent, ChannelVipRemoveCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelVIPRemove"/>.
/// </summary>
public record ChannelVipRemoveCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelVIPRemove"/> event.
/// </summary>
public record ChannelVipRemoveEvent : IHaveBroadcaster, IHaveUser
{
    /// <summary>
    /// The id of the user removed as a VIP.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user removed as a VIP.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user removed as a VIP.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the VIP was removed.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the VIP was removed.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the VIP was removed.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
}
