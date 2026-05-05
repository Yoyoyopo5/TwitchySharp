using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains static definitions for block reasons.
/// </summary>
/// <param name="Value">The string value of the block reason.</param>
[Wrapper<string>]
public readonly partial record struct BlockUserReason(string Value)
{
    public static BlockUserReason Harassment { get; } = new("harassment");
    public static BlockUserReason Spam { get; } = new("spam");
    public static BlockUserReason Other { get; } = new("other");
}
