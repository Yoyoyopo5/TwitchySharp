using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models;

namespace TwitchySharp.EventSub.Notifications.Channel.ChannelPoints;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsCustomRewardAdd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_custom_rewardadd">Channel Points Custom Reward Add</see> for more information.
/// </remarks>
public record ChannelPointsCustomRewardAddNotification : EventSubNotification<ChannelPointsCustomRewardAddEvent, ChannelPointsCustomRewardAddCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardAdd"/>.
/// </summary>
public record ChannelPointsCustomRewardAddCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardAdd"/> event.
/// </summary>
public record ChannelPointsCustomRewardAddEvent : ChannelPointsCustomRewardEvent;
