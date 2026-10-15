namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelCustomPowerUpRedemptionAdd"/> event.
/// </summary>
public record ChannelCustomPowerUpRedemptionAddEvent
{
    /// <summary>
    /// The id of the redemption.
    /// </summary>
    public required CustomPowerUpRedemptionId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the redemption was for.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login of the broadcaster (channel) that the redemption was for.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the redemption was for.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that made the redemption.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login of the user that made the redemption.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that made the redemption.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The user's input for the redemption.
    /// </summary>
    /// <remarks>
    /// If the power-up does not require input, this is <see cref="string.Empty"/>
    /// </remarks>
    // Not sure if this can be null if the power-up doesn't require input,
    // so I'm leaving nullable for safety now until we can test it.
    public string? UserInput { get; init; }
    /// <summary>
    /// The status of the redemption.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="ChannelPointsCustomRewardRedemptionStatus.Unfulfilled"/>.
    /// </remarks>
    public required ChannelPointsCustomRewardRedemptionStatus Status { get; init; }
    /// <summary>
    /// The custom power-up that was redeemed.
    /// </summary>
    public required RedemptionCustomPowerUp CustomPowerUp { get; init; }
    /// <summary>
    /// The time when the power-up was redeemed.
    /// </summary>
    public required DateTimeOffset RedeemedAt { get; init; }

}
