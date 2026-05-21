namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Unban"/> or <see cref="ChannelModerateActionType.Unban"/> action.
/// </summary>
public record ChannelModerateUnbanAction
{
    /// <summary>
    /// The id of the user that was unbanned.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that was unbanned.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that was unbanned.
    /// </summary>
    public required UserName UserName { get; init; }
}
