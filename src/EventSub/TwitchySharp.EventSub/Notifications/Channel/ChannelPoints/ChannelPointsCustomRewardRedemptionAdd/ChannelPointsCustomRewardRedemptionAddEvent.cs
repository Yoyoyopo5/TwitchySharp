namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionAdd"/> event.
/// </summary>
public record ChannelPointsCustomRewardRedemptionAddEvent
{
    /// <summary>
    /// The id of the redemption.
    /// </summary>
    public required RewardRedemptionId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the redeemed reward belongs to.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the redeemed reward belongs to.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the redeemed reward belongs to.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that redeemed the reward.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that redeemed the reward.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that redeemed the reward.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The user input provided. Empty string if not provided.
    /// </summary>
    public required string UserInput { get; init; }
    /// <summary>
    /// The reward redemption status. Defaults to <see cref="ChannelPointsCustomRewardRedemptionStatus.Unfulfilled"/>.
    /// </summary>
    public required ChannelPointsCustomRewardRedemptionStatus Status { get; init; }
    /// <summary>
    /// Information about the reward that was redeemed at the time it was redeemed.
    /// </summary>
    public required ChannelPointsCustomReward Reward { get; init; }
    /// <summary>
    /// The time when the redemption occurred.
    /// </summary>
    public required DateTimeOffset RedeemedAt { get; init; }
}
