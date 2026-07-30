namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsCustomRewardRedemptionUpdate"/> event.
/// </summary>
public record ChannelPointsCustomRewardRedemptionUpdateEvent
{
    /// <summary>
    /// The id of the redemption.
    /// </summary>
    public required RewardRedemptionId Id { get; init; }
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
    /// <summary>
    /// The user input provided. Empty string if not provided.
    /// </summary>
    public required string UserInput { get; init; }
    /// <summary>
    /// The reward redemption status after the update.
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
