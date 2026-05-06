using TwitchySharp.EventSub.Enums.Events.Channel;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Unraid"/> action.
/// </summary>
public record ChannelModerateUnraidAction : IHaveUser
{
    /// <summary>
    /// The user id of the broadcaster (channel) no longer being raided.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) no longer being raided.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) no longer being raided.
    /// </summary>
    public required string UserName { get; init; }
}
