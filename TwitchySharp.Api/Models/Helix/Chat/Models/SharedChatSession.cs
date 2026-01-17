using System;

namespace TwitchySharp.Api.Models.Helix.Chat.Models;

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