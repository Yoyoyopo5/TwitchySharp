using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events;

/// <summary>
/// Contains static definitions of possible emote formats.
/// </summary>
/// <param name="Value"></param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChatMessageEmoteFormat, string>))]
public record ChatMessageEmoteFormat(string Value)
    : ValueBackedEnum<string>(Value)
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
