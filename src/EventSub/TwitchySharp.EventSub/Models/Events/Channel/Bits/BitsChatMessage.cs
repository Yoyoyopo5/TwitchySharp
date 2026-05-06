using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel.Bits;

/// <summary>
/// Contains information about a chat message that was part of a specific Bits cheer.
/// </summary>
public record BitsChatMessage : IChatMessage
{
    public required string Text { get; init; }
    public required BitsChatMessageFragment[] Fragments { get; init; }
    IEnumerable<IChatMessageFragment> IChatMessage.Fragments => Fragments;
}
