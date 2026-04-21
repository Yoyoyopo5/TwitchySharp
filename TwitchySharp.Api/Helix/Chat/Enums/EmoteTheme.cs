using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Chat;

/// <summary>
/// Contains static definitions for possible emote background themes.
/// </summary>
/// <param name="Value">The string value of the emote theme.</param>
[Wrapper<string>]
public readonly partial record struct EmoteTheme(string Value)
{
    public static EmoteTheme Dark { get; } = new("dark");
    public static EmoteTheme Light { get; } = new("light");
}
