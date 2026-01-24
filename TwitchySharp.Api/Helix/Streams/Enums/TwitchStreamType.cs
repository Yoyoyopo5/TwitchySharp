using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Streams;

/// <summary>
/// Contains static definitions for possible Twitch stream types.
/// </summary>
/// <param name="Value">The string value of the stream type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<TwitchStreamType, string>))]
public record TwitchStreamType(string Value) : ValueBackedEnum<string>(Value)
{
    public static TwitchStreamType Live { get; } = new("live");
}