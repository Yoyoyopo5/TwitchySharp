using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains static definitions of possible emote formats.
/// </summary>
/// <param name="Value"></param>
[Wrapper<string>]
public readonly partial record struct ChatMessageEmoteFormat(string Value)
{
    /// <summary>
    /// An animated GIF.
    /// </summary>
    public static ChatMessageEmoteFormat Animated { get; } = new("animated");
    /// <summary>
    /// A static PNG.
    /// </summary>
    public static ChatMessageEmoteFormat Static { get; } = new("static");
}
