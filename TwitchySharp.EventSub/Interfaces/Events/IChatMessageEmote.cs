using TwitchySharp.EventSub.Enums.Events;

namespace TwitchySharp.EventSub.Interfaces.Events;

/// <summary>
/// An emote appearing in a Twitch chat message.
/// </summary>
public interface IChatMessageEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    string Id { get; }
    /// <summary>
    /// The id of the emote set that the emote belongs to.
    /// </summary>
    string EmoteSetId { get; }
    /// <summary>
    /// The user id of the broadcaster (channel) who owns the emote.
    /// </summary>
    string OwnerId { get; }
    /// <summary>
    /// The formats that the emote is available in.
    /// </summary>
    IEnumerable<ChatMessageEmoteFormat> Format { get; }
}
