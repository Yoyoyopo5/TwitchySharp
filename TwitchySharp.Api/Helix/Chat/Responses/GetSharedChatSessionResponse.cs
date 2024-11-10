using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of shared chat sessions.
/// </summary>
public record GetSharedChatSessionResponse
{
    /// <summary>
    /// A list containing the single shared chat session.
    /// </summary>
    public required SharedChatSession[] Data { get; init; }
}

/// <summary>
/// Contains information about a shared chat session.
/// </summary>
public record SharedChatSession
{
    /// <summary>
    /// The unique id for the shared chat session.
    /// </summary>
    public required string SessionId { get; init; }
    /// <summary>
    /// The user id of the host broadcaster.
    /// </summary>
    public required string HostBroadcasterId { get; init; }
    /// <summary>
    /// The list of participant broadcasters in the shared chat session.
    /// </summary>
    public required SharedChatParticipant[] Participants { get; init; }
    /// <summary>
    /// The date and time when the session was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// The date and time when the session was last updated.
    /// </summary>
    public required DateTimeOffset UpdatedAt { get; init; }
}

/// <summary>
/// Contains information about a broadcaster in a shared chat session.
/// </summary>
public record SharedChatParticipant
{
    /// <summary>
    /// The user id of the participant broadcaster.
    /// </summary>
    public required string BroadcasterId { get; init; }
}
