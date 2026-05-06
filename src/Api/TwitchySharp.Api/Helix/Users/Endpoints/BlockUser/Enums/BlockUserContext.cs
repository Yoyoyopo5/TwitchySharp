using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains static definitions for block contexts.
/// </summary>
/// <param name="Value">The string value of the block context.</param>
[Wrapper<string>]
public readonly partial record struct BlockUserContext(string Value)
{
    public static BlockUserContext Chat { get; } = new("chat");
    public static BlockUserContext Whisper { get; } = new("whisper");
}
