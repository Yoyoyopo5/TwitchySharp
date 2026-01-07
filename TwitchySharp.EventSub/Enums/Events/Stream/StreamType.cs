using TwitchySharp.Helpers;
using System.Text.Json.Serialization;

namespace TwitchySharp.EventSub.Enums.Events.Stream;

/// <summary>
/// Contains static definitions for possible Stream types.
/// </summary>
/// <param name="Value">The string value for the Stream type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<StreamType, string>))]
public record StreamType(string Value) : ValueBackedEnum<string>(Value)
{
    public static StreamType Live { get; } = new("live");
    public static StreamType Playlist { get; } = new("playlist");
    public static StreamType WatchParty { get; } = new("watch_party");
    public static StreamType Premiere { get; } = new("premiere");
    public static StreamType Rerun { get; } = new("rerun");
}
