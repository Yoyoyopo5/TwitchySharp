namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Untimeout"/> or <see cref="ChannelModerateActionType.SharedChatUntimeout"/> action.
/// </summary>
public record ChannelModerateUntimeoutAction
{
    /// <summary>
    /// The id of the user that was untimed out.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that was untimed out.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that was untimed out.
    /// </summary>
    public required UserName UserName { get; init; }
}
