
namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains information about a broadcaster in a shared chat session.
/// </summary>
public record SharedChatParticipant
{
    /// <summary>
    /// The user id of the participant broadcaster.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
}
