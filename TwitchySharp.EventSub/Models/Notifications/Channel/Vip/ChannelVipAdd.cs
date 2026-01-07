using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.Vip;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelVipAdd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelvipadd">Channel VIP Add</see> for more information.
/// </remarks>
public record ChannelVipAddNotification : EventSubNotification<ChannelVipAddEvent, ChannelVipAddCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelVIPAdd"/>.
/// </summary>
public record ChannelVipAddCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelVIPAdd"/> event.
/// </summary>
public record ChannelVipAddEvent : IHaveBroadcaster, IHaveUser
{
    /// <summary>
    /// The id of the user added as a VIP.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user added as a VIP.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user added as a VIP.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the VIP was added.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the VIP was added.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the VIP was added.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
}
