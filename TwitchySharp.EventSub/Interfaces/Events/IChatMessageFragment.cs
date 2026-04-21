using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Interfaces.Events;

/// <summary>
/// A fragment of a Twitch chat message.
/// </summary>
public interface IChatMessageFragment
{
    /// <summary>
    /// The fragment in string format.
    /// </summary>
    string Text { get; }
    /// <summary>
    /// The type of fragment.
    /// </summary>
    string Type { get; }
    /// <summary>
    /// The emote information.
    /// This is <see langword="null"/> unless <see cref="Type"/> is <c>emote</c>.
    /// </summary>
    IChatMessageEmote? Emote { get; }
    /// <summary>
    /// The cheermote information.
    /// This is <see langword="null"/> unless <see cref="Type"/> is <c>cheermote</c>.
    /// </summary>
    IChatMessageCheermote? Cheermote { get; }
    /// <summary>
    /// The mention information.
    /// This is <see langword="null"/> unless <see cref="Type"/> is <c>mention</c>.
    /// </summary>
    IChatMessageMention? Mention { get; }
}