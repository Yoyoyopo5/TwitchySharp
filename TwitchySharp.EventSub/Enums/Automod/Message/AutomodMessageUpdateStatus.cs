using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Automod.Message;

/// <summary>
/// Represents the status of an updated automod message.
/// </summary>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<AutomodMessageUpdateStatus, string>))]
public record AutomodMessageUpdateStatus(string Value) : ValueBackedEnum<string>(Value)
{
    public static AutomodMessageUpdateStatus Approved { get; } = new("approved");
    public static AutomodMessageUpdateStatus Denied { get; } = new("denied");
    public static AutomodMessageUpdateStatus Expired { get; } = new("expired");
}
