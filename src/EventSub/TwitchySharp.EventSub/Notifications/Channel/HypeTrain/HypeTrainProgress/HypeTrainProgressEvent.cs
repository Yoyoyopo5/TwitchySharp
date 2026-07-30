namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.HypeTrainProgress"/> event.
/// </summary>
public record HypeTrainProgressEvent
{
    /// <summary>
    /// The id of the Hype Train.
    /// </summary>
    public required HypeTrainId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) hosting the Hype Train.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) hosting the Hype Train.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) hosting the Hype Train.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The total amount of points contributed to the Hype Train.
    /// </summary>
    public required HypeTrainPointCount Total { get; init; }
    /// <summary>
    /// The current level of the Hype Train.
    /// </summary>
    public required HypeTrainLevel Level { get; init; }
    /// <summary>
    /// Contains the list of broadcasters in the shared Hype Train, or <see langword="null"/> if the Hype Train is not shared.
    /// </summary>
    public SharedHypeTrainParticipant[]? SharedTrainParticipants { get; init; }
    /// <summary>
    /// The date and time when the Hype Train began.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The type of Hype Train.
    /// </summary>
    public required HypeTrainType Type { get; init; }
    /// <summary>
    /// Indicates whether the Hype Train is shared.
    /// </summary>
    public required bool IsSharedTrain { get; init; }
    /// <summary>
    /// The number of points contributed to the Hype Train at its current level.
    /// </summary>
    public required HypeTrainPointCount Progress { get; init; }
    /// <summary>
    /// The number of points required to reach the next level.
    /// </summary>
    public required HypeTrainPointCount Goal { get; init; }
    /// <summary>
    /// The contributors with the most points contributed.
    /// </summary>
    public required HypeTrainTopContributor TopContributions { get; init; }
    /// <summary>
    /// The date and time when the Hype Train will expire.
    /// </summary>
    /// <remarks>
    /// This is extended when the Hype Train reaches a new level.
    /// </remarks>
    public DateTimeOffset ExpiresAt { get; init; }
}
