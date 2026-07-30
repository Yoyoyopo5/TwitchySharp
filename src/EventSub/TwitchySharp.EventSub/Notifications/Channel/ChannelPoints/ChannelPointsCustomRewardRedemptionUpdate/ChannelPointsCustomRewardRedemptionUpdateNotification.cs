namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_custom_reward_redemptionupdate">Channel Points Custom Reward Redemption Update</see> for more information.
/// </remarks>
public record ChannelPointsCustomRewardRedemptionUpdateNotification : EventSubNotification<ChannelPointsCustomRewardRedemptionUpdateEvent, ChannelPointsCustomRewardRedemptionUpdateCondition>;
