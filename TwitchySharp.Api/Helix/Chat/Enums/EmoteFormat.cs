using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains static definitions for possible emote formats.
/// </summary>
/// <param name="Value">The string value of the emote format.</param>
[Wrapper<string>]
public readonly partial record struct EmoteFormat(string Value)
{
    public static EmoteFormat Animated { get; } = new("animated");
    public static EmoteFormat Static { get; } = new("static");
}
