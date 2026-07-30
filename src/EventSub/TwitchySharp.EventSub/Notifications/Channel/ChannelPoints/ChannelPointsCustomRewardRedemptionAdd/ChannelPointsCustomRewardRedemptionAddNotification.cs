namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_custom_reward_redemptionadd">Channel Points Custom Reward Redemption Add</see> for more information.
/// </remarks>
public record ChannelPointsCustomRewardRedemptionAddNotification : EventSubNotification<ChannelPointsCustomRewardRedemptionAddEvent, ChannelPointsCustomRewardRedemptionAddCondition>;
