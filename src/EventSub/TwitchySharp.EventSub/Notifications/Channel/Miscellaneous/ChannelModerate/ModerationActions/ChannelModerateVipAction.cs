namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Vip"/> action.
/// </summary>
public record ChannelModerateVipAction
{
    /// <summary>
    /// The id of the user gaining VIP status.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user gaining VIP status.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user gaining VIP status.
    /// </summary>
    public required UserName UserName { get; init; }
}
