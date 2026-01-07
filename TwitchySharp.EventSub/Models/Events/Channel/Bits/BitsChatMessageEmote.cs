using TwitchySharp.EventSub.Enums.Events;
using TwitchySharp.EventSub.Interfaces.Events;

namespace TwitchySharp.EventSub.Models.Events.Channel.Bits;

/// <summary>
/// Contains information about a specific emote used in a Bits cheer chat message.
/// </summary>
public record BitsChatMessageEmote : IChatMessageEmote
{
    public required string Id { get; init; }
    public required string EmoteSetId { get; init; }
    public required string OwnerId { get; init; }
    public required ChatMessageEmoteFormat[] Format { get; init; }
    IEnumerable<ChatMessageEmoteFormat> IChatMessageEmote.Format => Format;
}
