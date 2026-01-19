using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains static definitions for possible Twitch user types.
/// These are used to distinguish Twitch staff from regular users.
/// </summary>
/// <param name="Value">The string value of the user type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<TwitchUserType, string>))]
public record TwitchUserType(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static TwitchUserType Admin { get; } = new("admin");
    public static TwitchUserType GlobalMod { get; } = new("global_mod");
    public static TwitchUserType Staff { get; } = new("staff");
    public static TwitchUserType Normal { get; } = new(string.Empty);
}
