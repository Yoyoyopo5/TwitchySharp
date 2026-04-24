using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Enums.Events.Automod.Message;

/// <summary>
/// Represents the status of an updated automod message.
/// </summary>
[Wrapper<string>]
public readonly partial record struct AutomodMessageUpdateStatus(string Value)
{
    public static AutomodMessageUpdateStatus Approved { get; } = new("approved");
    public static AutomodMessageUpdateStatus Denied { get; } = new("denied");
    public static AutomodMessageUpdateStatus Expired { get; } = new("expired");
}
