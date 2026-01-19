using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains static definitions for possible broadcaster types.
/// </summary>
/// <param name="Value">The string value of the broadcaster type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<TwitchBroadcasterType, string>))]
public record TwitchBroadcasterType(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static TwitchBroadcasterType Affiliate { get; } = new("affiliate");
    public static TwitchBroadcasterType Partner { get; } = new("partner");
    public static TwitchBroadcasterType Normal { get; } = new(string.Empty);
}
