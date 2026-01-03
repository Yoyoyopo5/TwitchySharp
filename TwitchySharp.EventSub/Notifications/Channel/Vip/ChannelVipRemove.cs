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
public record ChannelVipRemoveEvent : ChannelVipEvent;
