namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.ApproveUnbanRequest"/> or <see cref="ChannelModerateActionType.DenyUnbanRequest"/> action.
/// </summary>
public record ChannelModerateUnbanRequestAction
{
    /// <summary>
    /// Indicates whether the unban request was approved or denied.
    /// </summary>
    public required bool IsApproved { get; init; }
    /// <summary>
    /// The id of the user that created the unban request.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that created the unban request.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that created the unban request.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The moderator-provided message explaining the unban request response.
    /// </summary>
    public string? ModeratorMessage { get; init; } // Pretty sure this is optional, although not indicated in docs.
}
