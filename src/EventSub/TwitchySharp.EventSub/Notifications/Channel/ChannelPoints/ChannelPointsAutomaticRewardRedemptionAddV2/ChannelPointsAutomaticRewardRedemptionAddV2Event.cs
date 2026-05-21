namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPointsAutomaticRewardRedemptionAddV2"/> event.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-reference/#channel-points-automatic-reward-redemption-add-v2-event">Channel Points Automatic Reward Redemption Add V2 Event</see> for more information.
/// </remarks>
public record ChannelPointsAutomaticRewardRedemptionAddV2Event
{
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
