using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains static definitions for possible suspicious user statuses.
/// </summary>
/// <param name="Value">The string value of the suspicious user status.</param>
[Wrapper<string>]
public readonly partial record struct SuspiciousUserStatus(string Value)
{
    public static SuspiciousUserStatus ActiveMonitoring { get; } = new("ACTIVE_MONITORING");
    public static SuspiciousUserStatus Restricted { get; } = new("RESTRICTED");
}
