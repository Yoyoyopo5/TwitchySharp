namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Unvip"/> action.
/// </summary>
public record ChannelModerateUnvipAction
{
    /// <summary>
    /// The id of the user losing VIP status.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user losing VIP status.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user losing VIP status.
    /// </summary>
    public required UserName UserName { get; init; }
}
