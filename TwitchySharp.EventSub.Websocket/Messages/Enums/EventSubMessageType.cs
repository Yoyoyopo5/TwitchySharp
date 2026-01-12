using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Websocket.Messages.Enums;
/// <summary>
/// Contains static definitions for possible Twitch EventSub message types.
/// </summary>
/// <param name="Value">The string value of the message type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<EventSubMessageType, string>))]
public record EventSubMessageType(string Value) : ValueBackedEnum<string>(Value)
{
    public static EventSubMessageType Welcome { get; } = new("session_welcome");
    public static EventSubMessageType Keepalive { get; } = new("session_keepalive");
    public static EventSubMessageType Notification { get; } = new("notification");
    public static EventSubMessageType Reconnect { get; } = new("session_reconnect");
    public static EventSubMessageType Recovation { get; } = new("revocation");
}
