namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsCustomRewardRemove"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_custom_rewardremove">Channel Points Custom Reward Remove</see> for more information.
/// </remarks>
public record ChannelPointsCustomRewardRemoveNotification : EventSubNotification<ChannelPointsCustomRewardRemoveEvent, ChannelPointsCustomRewardRemoveCondition>;
