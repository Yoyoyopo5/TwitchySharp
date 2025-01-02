using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains information about a warning issued to a user in chat.
/// </summary>
public record WarnChatUserResponse
{
    /// <summary>
    /// A list containing a single object describing the warning that was issued.
    /// </summary>
    public required IssuedChatUserWarning[] Data { get; init; }
}

/// <summary>
/// Contains information about a specific warning given to a user in a channel's chatroom.
/// </summary>
public record IssuedChatUserWarning
{
    /// <summary>
    /// The user id of the broadcaster (channel) in which the warning was issued.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The id of the user that was issued the warning.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The user id of the moderator that issued the warning.
    /// </summary>
    public required string ModeratorId { get; init; }
    /// <summary>
    /// The moderator-supplied reason for the warning.
    /// </summary>
    public required string Reason { get; init; }
}
