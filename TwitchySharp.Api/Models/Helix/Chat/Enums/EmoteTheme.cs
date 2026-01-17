using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Chat.Enums;

/// <summary>
/// Contains static definitions for possible emote background themes.
/// </summary>
/// <param name="Value">The string value of the emote theme.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<EmoteTheme, string>))]
public record EmoteTheme(string Value) : ValueBackedEnum<string>(Value)
{
    public static EmoteTheme Dark { get; } = new("dark");
    public static EmoteTheme Light { get; } = new("light");
}
