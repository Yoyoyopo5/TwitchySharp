using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Websocket.Messages.Enums;
/// <summary>
/// Contains static definitions for possible Twitch EventSub message types.
/// </summary>
/// <param name="Value">The string value of the message type.</param>
[Wrapper<string>]
public readonly partial record struct EventSubMessageType(string Value)
{
    public static EventSubMessageType Welcome { get; } = new("session_welcome");
    public static EventSubMessageType Keepalive { get; } = new("session_keepalive");
    public static EventSubMessageType Notification { get; } = new("notification");
    public static EventSubMessageType Reconnect { get; } = new("session_reconnect");
    public static EventSubMessageType Recovation { get; } = new("revocation");
}
