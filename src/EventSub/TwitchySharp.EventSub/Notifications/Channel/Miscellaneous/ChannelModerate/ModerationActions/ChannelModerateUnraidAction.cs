namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Unraid"/> action.
/// </summary>
public record ChannelModerateUnraidAction
{
    /// <summary>
    /// The user id of the broadcaster (channel) no longer being raided.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) no longer being raided.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) no longer being raided.
    /// </summary>
    public required UserName UserName { get; init; }
}
