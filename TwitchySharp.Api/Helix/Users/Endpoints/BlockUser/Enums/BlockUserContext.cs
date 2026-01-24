using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains static definitions for block contexts.
/// </summary>
/// <param name="Value">The string value of the block context.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<BlockUserContext, string>))]
public record BlockUserContext(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static BlockUserContext Chat { get; } = new("chat");
    public static BlockUserContext Whisper { get; } = new("whisper");
}
