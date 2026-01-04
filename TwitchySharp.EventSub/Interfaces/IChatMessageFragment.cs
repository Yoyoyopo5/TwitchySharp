using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Interfaces;

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
    ValueBackedEnum<string> Type { get; }
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