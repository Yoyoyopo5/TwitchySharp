using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains static definitions for possible Twitch user types.
/// These are used to distinguish Twitch staff from regular users.
/// </summary>
/// <param name="Value"></param>
public record TwitchUserType(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static TwitchUserType Admin { get; } = new("admin");
    public static TwitchUserType GlobalMod { get; } = new("global_mod");
    public static TwitchUserType Staff { get; } = new("staff");
    public static TwitchUserType Normal { get; } = new(string.Empty);
}
