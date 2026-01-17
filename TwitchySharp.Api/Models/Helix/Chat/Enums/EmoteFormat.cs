using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Chat.Enums;
/// <summary>
/// Contains static definitions for possible emote formats.
/// </summary>
/// <param name="Value">The string value of the emote format.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<EmoteFormat, string>))]
public record EmoteFormat(string Value) : ValueBackedEnum<string>(Value)
{
    public static EmoteFormat Animated { get; } = new("animated");
    public static EmoteFormat Static { get; } = new("static");
}
