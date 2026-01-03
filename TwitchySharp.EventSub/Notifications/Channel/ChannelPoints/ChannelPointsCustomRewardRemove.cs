using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.ChannelPoints;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsCustomRewardRemove"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_custom_rewardremove">Channel Points Custom Reward Remove</see> for more information.
/// </remarks>
public record ChannelPointsCustomRewardRemoveNotification : EventSubNotification<ChannelPointsCustomRewardRemoveEvent, ChannelPointsCustomRewardRemoveCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardRemove"/>.
/// </summary>
public record ChannelPointsCustomRewardRemoveCondition : BroadcasterRewardCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardRemove"/> event.
/// </summary>
public record ChannelPointsCustomRewardRemoveEvent : ChannelPointsCustomRewardEvent;
