namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsCustomRewardAdd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_custom_rewardadd">Channel Points Custom Reward Add</see> for more information.
/// </remarks>
public record ChannelPointsCustomRewardAddNotification : EventSubNotification<ChannelPointsCustomRewardAddEvent, ChannelPointsCustomRewardAddCondition>;
