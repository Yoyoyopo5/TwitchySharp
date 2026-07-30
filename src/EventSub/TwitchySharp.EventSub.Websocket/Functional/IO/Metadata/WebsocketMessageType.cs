using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.EventSub.Websocket.Functional;
/// <summary>
/// Contains static definitions for possible Twitch EventSub message types.
/// </summary>
/// <param name="Value">The string value of the message type.</param>
[Wrapper<string>]
public readonly partial record struct WebsocketMessageType(string Value)
{
    public static WebsocketMessageType Welcome { get; } = new(WebsocketMessageTypes.WELCOME);
    public static WebsocketMessageType Keepalive { get; } = new(WebsocketMessageTypes.KEEPALIVE);
    public static WebsocketMessageType Notification { get; } = new(WebsocketMessageTypes.NOTIFICATION);
    public static WebsocketMessageType Reconnect { get; } = new(WebsocketMessageTypes.RECONNECT);
    public static WebsocketMessageType Recovation { get; } = new(WebsocketMessageTypes.REVOCATION);
}
