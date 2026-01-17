namespace TwitchySharp.Api.Models.Helix.Chat.Models;

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
