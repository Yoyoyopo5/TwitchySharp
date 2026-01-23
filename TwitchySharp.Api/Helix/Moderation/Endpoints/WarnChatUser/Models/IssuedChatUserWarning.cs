using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains information about a specific warning given to a user in a channel's chatroom.
/// </summary>
public record IssuedChatUserWarning
{
    /// <summary>
    /// The user id of the broadcaster (channel) in which the warning was issued.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The id of the user that was issued the warning.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The user id of the moderator that issued the warning.
    /// </summary>
    public required UserId ModeratorId { get; init; }
    /// <summary>
    /// The moderator-supplied reason for the warning.
    /// </summary>
    public required string Reason { get; init; }
}
