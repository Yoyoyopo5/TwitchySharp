using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.ChannelPoints;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.ChannelPoints;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAdd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelchannel_points_automatic_reward_redemptionadd">Channel Points Automatic Reward Redemption Add</see> for more information.
/// </remarks>
public record ChannelPointsAutomaticRewardRedemptionAddNotification : EventSubNotification<ChannelPointsAutomaticRewardRedemptionAddEvent, ChannelPointsAutomaticRewardRedemptionAddCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAdd"/>.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionAddCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAdd"/> event.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionAddEvent : IHaveBroadcaster, IHaveUser, IHaveChannelPointsRewardRedemption
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
    /// The automatic (built-in) reward that was redeemed.
    /// </summary>
    public required ChannelPointsAutomaticRewardRedemptionReward Reward { get; init; }
    /// <summary>
    /// The message that was sent with the redemption.
    /// This is <see langword="null"/> if the reward does not require user input.
    /// </summary>
    public ChannelPointsRewardRedemptionMessage? Message { get; init; } // Almost certain this can be null
    /// <summary>
    /// The message that was sent with the redemption, in string format.
    /// This is <see langword="null"/> if the reward does not require user input.
    /// </summary>
    public string? UserInput { get; init; }
    /// <summary>
    /// The date and time when the reward was redeemed.
    /// </summary>
    public required DateTimeOffset RedeemedAt { get; init; }
}
