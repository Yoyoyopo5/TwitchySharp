using TwitchySharp.EventSub.Enums.Events.Channel;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.Timeout"/> or <see cref="ChannelModerateActionType.SharedChatTimeout"/> action.
/// </summary>
public record ChannelModerateTimeoutAction : IHaveUser
{
    /// <summary>
    /// The id of the user that was timed out.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that was timed out.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that was timed out.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The moderator-provided reason for the timeout, if any.
    /// </summary>
    public string? Reason { get; init; }
    /// <summary>
    /// The date and time at which the timeout will end.
    /// </summary>
    public required DateTimeOffset ExpiresAt { get; init; }
}
