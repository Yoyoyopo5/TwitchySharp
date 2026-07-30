namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsCustomRewardUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_custom_rewardupdate">Channel Points Custom Reward Update</see> for more information.
/// </remarks>
public record ChannelPointsCustomRewardUpdateNotification : EventSubNotification<ChannelPointsCustomRewardUpdateEvent, ChannelPointsCustomRewardUpdateCondition>;
