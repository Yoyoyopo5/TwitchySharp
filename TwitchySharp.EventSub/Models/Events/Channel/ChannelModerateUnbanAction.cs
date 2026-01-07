using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Enums.Events.Channel;

namespace TwitchySharp.EventSub.Models.Events.Channel;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Unban"/> or <see cref="ChannelModerateActionType.Unban"/> action.
/// </summary>
public record ChannelModerateUnbanAction : IHaveUser
{
    /// <summary>
    /// The id of the user that was unbanned.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that was unbanned.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that was unbanned.
    /// </summary>
    public required string UserName { get; init; }
}
