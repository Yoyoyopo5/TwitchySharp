namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Timeout"/> or <see cref="ChannelModerateActionType.SharedChatTimeout"/> action.
/// </summary>
public record ChannelModerateTimeoutAction
{
    /// <summary>
    /// The id of the user that was timed out.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that was timed out.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that was timed out.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The moderator-provided reason for the timeout, if any.
    /// </summary>
    public string? Reason { get; init; }
    /// <summary>
    /// The date and time at which the timeout will end.
    /// </summary>
    public required DateTimeOffset ExpiresAt { get; init; }
}
