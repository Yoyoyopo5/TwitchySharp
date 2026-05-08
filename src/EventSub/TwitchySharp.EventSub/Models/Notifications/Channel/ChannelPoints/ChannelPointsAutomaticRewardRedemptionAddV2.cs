using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.ChannelPoints;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.ChannelPoints;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_automatic_reward_redemptionadd-v2">Channel Points Automatic Reward Redemption Add V2</see> for more information.
/// </remarks>
public record ChannelPointsAutomaticRewardRedemptionAddV2Notification : EventSubNotification<ChannelPointsAutomaticRewardRedemptionAddV2Event, ChannelPointsAutomaticRewardRedemptionAddV2Condition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2"/>.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionAddV2Condition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2"/> event.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionAddV2Event : IHaveBroadcaster, IHaveUser, IHaveChannelPointsRewardRedemption
{
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) whose chat the reward was redeemed in.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) whose chat the reward was redeemed in.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) whose chat the reward was redeemed in.
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
    /// <summary>
    /// The reward that was redeemed.
    /// </summary>
    public required ChannelPointsAutomaticRewardRedemptionV2Reward Reward { get; init; }
    /// <summary>
    /// The chat message that was submitted with the redemption.
    /// </summary>
    public ChannelPointsRewardRedemptionMessageV2? Message { get; init; }
    /// <summary>
    /// The date and time when the reward was redeemed.
    /// </summary>
    public required DateTimeOffset RedeemedAt { get; init; }
}
