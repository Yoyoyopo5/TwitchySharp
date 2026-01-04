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
    IChatMessageEmote? Emote { get; }
    IChatMessageCheermote? Cheermote { get; }
}
