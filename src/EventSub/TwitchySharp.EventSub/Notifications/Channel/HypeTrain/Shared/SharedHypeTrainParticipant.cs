namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific broadcaster (channel) participating in a Hype Train in a shared chat.
/// </summary>
public record SharedHypeTrainParticipant
{
    /// <summary>
    /// The user id of the broadcaster participating in the shared chat Hype Train.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster participating in the shared chat Hype Train.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster participating in the shared chat Hype Train.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
}
