namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPredictionLock"/> event.
/// </summary>
public record ChannelPredictionLockEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) that is hosting the prediction.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that is hosting the prediction.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that is hosting the prediction.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the prediction.
    /// </summary>
    public required PredictionId Id { get; init; }
    /// <summary>
    /// The title of the prediction.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The outcomes of the prediction.
    /// </summary>
    public required ChannePredictionOutcome[] Outcomes { get; init; }
    /// <summary>
    /// The date and time the prediction started.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The date and time when the prediction locked (no more bets can be made).
    /// </summary>
    public required DateTimeOffset LockedAt { get; init; }
}
