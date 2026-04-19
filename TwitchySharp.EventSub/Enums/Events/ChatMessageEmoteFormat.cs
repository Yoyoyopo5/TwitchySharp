using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events;

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
