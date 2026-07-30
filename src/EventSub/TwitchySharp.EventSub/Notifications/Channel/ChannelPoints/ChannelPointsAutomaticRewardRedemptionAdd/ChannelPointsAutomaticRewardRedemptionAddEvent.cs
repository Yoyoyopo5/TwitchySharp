namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAdd"/> event.
/// </summary>
public record ChannelPointsAutomaticRewardRedemptionAddEvent
{
    /// <summary>
    /// The id of the redemption.
    /// </summary>
    public required RewardRedemptionId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) whose chat the reward was redeemed in.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) whose chat the reward was redeemed in.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) whose chat the reward was redeemed in.
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
