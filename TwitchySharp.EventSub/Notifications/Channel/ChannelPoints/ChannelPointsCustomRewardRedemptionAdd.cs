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
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_custom_reward_redemptionadd">Channel Points Custom Reward Redemption Add</see> for more information.
/// </remarks>
public record ChannelPointsCustomRewardRedemptionAddNotification : EventSubNotification<ChannelPointsCustomRewardRedemptionAddEvent, ChannelPointsCustomRewardRedemptionAddCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd"/>.
/// </summary>
public record ChannelPointsCustomRewardRedemptionAddCondition : BroadcasterRewardCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd"/> event.
/// </summary>
public record ChannelPointsCustomRewardRedemptionAddEvent : ChannelPointsCustomRewardRedemptionEvent;
