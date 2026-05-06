using TwitchySharp.EventSub.Enums.Events.Channel;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Unmod"/> action.
/// </summary>
public record ChannelModerateUnmodAction : IHaveUser
{
    /// <summary>
    /// The id of the user losing moderator status.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user losing moderator status.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user losing moderator status.
    /// </summary>
    public required string UserName { get; init; }
}
