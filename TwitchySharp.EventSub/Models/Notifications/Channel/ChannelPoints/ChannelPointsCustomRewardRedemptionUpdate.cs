using TwitchySharp.EventSub.Enums.Events.Channel.ChannelPoints;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.ChannelPoints;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.ChannelPoints;

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
public record ChannelPointsCustomRewardRedemptionUpdateEvent : IHaveChannelPointsCustomRewardRedemption, IHaveBroadcaster, IHaveUser
{
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the redeemed reward belongs to.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the redeemed reward belongs to.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the redeemed reward belongs to.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that redeemed the reward.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that redeemed the reward.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that redeemed the reward.
    /// </summary>
    public required string UserName { get; init; }
    public required string UserInput { get; init; }
    public required ChannelPointsCustomRewardRedemptionStatus Status { get; init; }
    public required ChannelPointsCustomReward Reward { get; init; }
    public required DateTimeOffset RedeemedAt { get; init; }
}
