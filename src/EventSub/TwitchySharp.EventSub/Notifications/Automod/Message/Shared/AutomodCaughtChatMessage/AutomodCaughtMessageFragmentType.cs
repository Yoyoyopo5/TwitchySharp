using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains static definitions for potential Automod message fragment types.
/// </summary>
/// <param name="Value">The string value for the Automod message fragment type.</param>
[Wrapper<string>]
public readonly partial record struct AutomodCaughtMessageFragmentType(string Value)
{
    /// <summary>
    /// A text fragment.
    /// </summary>
    public static AutomodCaughtMessageFragmentType Text { get; } = new("text");
    /// <summary>
    /// An emote fragment.
    /// </summary>
    public static AutomodCaughtMessageFragmentType Emote { get; } = new("emote");
    /// <summary>
    /// A bits cheermote fragment.
    /// </summary>
    public static AutomodCaughtMessageFragmentType Cheermote { get; } = new("cheermote");
}
