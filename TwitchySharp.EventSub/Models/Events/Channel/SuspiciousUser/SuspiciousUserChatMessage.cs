using TwitchySharp.EventSub.Interfaces;

namespace TwitchySharp.EventSub.Models.Events.Channel.SuspiciousUser;

/// <summary>
/// Contains information about a specific chat message from a suspicious user.
/// </summary>
public record SuspiciousUserChatMessage : IChatMessage
{
    /// <summary>
    /// The id of the message.
    /// </summary>
    public required string MessageId { get; init; }
    public required string Text { get; init; }
    public required SuspiciousUserChatMessageFragment[] Fragments { get; init; }
    IEnumerable<IChatMessageFragment> IChatMessage.Fragments => Fragments;
}