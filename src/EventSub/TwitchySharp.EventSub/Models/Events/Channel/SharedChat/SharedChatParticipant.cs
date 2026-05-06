using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel.SharedChat;

/// <summary>
/// Contains information about a specific broadcaster participant in a shared chat session.
/// </summary>
public record SharedChatParticipant : IHaveBroadcaster
{
    /// <summary>
    /// The user id of the broadcaster (channel) that is participating in the shared chat session.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that is participating in the shared chat session.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that is participating in the shared chat session.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
}
