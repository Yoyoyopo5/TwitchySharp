namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_automatic_reward_redemptionadd-v2">Channel Points Automatic Reward Redemption Add V2</see> for more information.
/// </remarks>
public record ChannelPointsAutomaticRewardRedemptionAddV2Notification : EventSubNotification<ChannelPointsAutomaticRewardRedemptionAddV2Event, ChannelPointsAutomaticRewardRedemptionAddV2Condition>;
