namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAdd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_automatic_reward_redemptionadd">Channel Points Automatic Reward Redemption Add</see> for more information.
/// </remarks>
public record ChannelPointsAutomaticRewardRedemptionAddNotification : EventSubNotification<ChannelPointsAutomaticRewardRedemptionAddEvent, ChannelPointsAutomaticRewardRedemptionAddCondition>;
