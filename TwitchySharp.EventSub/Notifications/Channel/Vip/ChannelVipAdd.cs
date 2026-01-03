using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.Vip;
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
public record ChannelVipAddEvent : ChannelVipEvent;
