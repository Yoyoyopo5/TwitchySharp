using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains static definitions for possible suspicious user statuses.
/// </summary>
/// <param name="Value">The string value of the suspicious user status.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<SuspiciousUserStatus, string>))]
public record SuspiciousUserStatus(string Value) : ValueBackedEnum<string>(Value)
{
    public static SuspiciousUserStatus ActiveMonitoring { get; } = new("ACTIVE_MONITORING");
    public static SuspiciousUserStatus Restricted { get; } = new("RESTRICTED");
}