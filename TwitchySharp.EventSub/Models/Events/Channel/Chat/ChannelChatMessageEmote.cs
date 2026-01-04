using TwitchySharp.EventSub.Enums;
using TwitchySharp.EventSub.Interfaces;

namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

/// <summary>
/// Contains information about a specific emote in a chat message.
/// </summary>
public record ChannelChatMessageEmote : IChatMessageEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The id of the set the emote belongs to.
    /// </summary>
    public required string EmoteSetId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) who owns the emote.
    /// </summary>
    public required string OwnerId { get; init; }
    /// <summary>
    /// The formats the emote is available in.
    /// </summary>
    public required ChatMessageEmoteFormat[] Format { get; init; }
    IEnumerable<ChatMessageEmoteFormat> IChatMessageEmote.Format => Format;
}
