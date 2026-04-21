using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Enums.Events.Channel.SuspiciousUser;

/// <summary>
/// Contains static definitions for possible suspicious user statuses.
/// </summary>
/// <param name="Value">The string value of the suspicious user status.</param>
[Wrapper<string>]
public readonly partial record struct SuspiciousUserStatus(string Value)
{
    public static SuspiciousUserStatus None { get; } = new("none");
    public static SuspiciousUserStatus ActiveMonitoring { get; } = new("active_monitoring");
    public static SuspiciousUserStatus Restricted { get; } = new("restricted");
}
