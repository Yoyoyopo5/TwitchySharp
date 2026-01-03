using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models;

namespace TwitchySharp.EventSub.Notifications.Channel.ChannelPoints;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_custom_reward_redemptionupdate">Channel Points Custom Reward Redemption Update</see> for more information.
/// </remarks>
public record ChannelPointsCustomRewardRedemptionUpdateNotification : EventSubNotification<ChannelPointsCustomRewardRedemptionUpdateEvent, ChannelPointsCustomRewardRedemptionUpdateCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionUpdate"/>.
/// </summary>
public record ChannelPointsCustomRewardRedemptionUpdateCondition : BroadcasterRewardCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionUpdate"/> event.
/// </summary>
public record ChannelPointsCustomRewardRedemptionUpdateEvent : ChannelPointsCustomRewardRedemptionEvent;
