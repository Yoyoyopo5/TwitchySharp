namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Warn"/> action.
/// </summary>
public record ChannelModerateWarnAction
{
    /// <summary>
    /// The id of the user being warned.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user being warned.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user being warned.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The reason given for the warning.
    /// </summary>
    public string? Reason { get; init; }
    /// <summary>
    /// Chat rules cited for the warning.
    /// </summary>
    public string[]? ChatRulesCited { get; init; }
}
