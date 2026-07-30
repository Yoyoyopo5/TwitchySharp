namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Ban"/> or <see cref="ChannelModerateActionType.SharedChatBan"/> action.
/// </summary>
public record ChannelModerateBanAction
{
    /// <summary>
    /// The id of the user that was banned.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that was banned.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that was banned.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The moderator-provided reason for the ban, if any.
    /// </summary>
    public string? Reason { get; init; }
}
