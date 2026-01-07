using TwitchySharp.EventSub.Enums;
using TwitchySharp.EventSub.Interfaces;

namespace TwitchySharp.EventSub.Models.Events.Channel.SuspiciousUser;

/// <summary>
/// Contains information about an emote used in a suspicious user chat message.
/// </summary>
public record SuspiciousUserChatMessageEmote : IChatMessageEmote
{
    public required string Id { get; init; }
    public required string EmoteSetId { get; init; }
    /// <summary>
    /// Not supported for this event type.
    /// Defaults to <see cref="string.Empty"/>.
    /// </summary>
    string IChatMessageEmote.OwnerId => string.Empty;
    /// <summary>
    /// Not supported for this event type.
    /// Defaults to <see cref="Array.Empty{T}"/>.
    /// </summary>
    IEnumerable<ChatMessageEmoteFormat> IChatMessageEmote.Format => [];
}
