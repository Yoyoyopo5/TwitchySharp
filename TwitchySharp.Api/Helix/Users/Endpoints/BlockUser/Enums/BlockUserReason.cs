using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Users;

/// <summary>
/// Contains static definitions for block reasons.
/// </summary>
/// <param name="Value">The string value of the block reason.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<BlockUserReason, string>))]
public record BlockUserReason(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static BlockUserReason Harassment { get; } = new("harassment");
    public static BlockUserReason Spam { get; } = new("spam");
    public static BlockUserReason Other { get; } = new("other");
}
