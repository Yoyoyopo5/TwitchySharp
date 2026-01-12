namespace TwitchySharp.EventSub.Websocket.Messages.Enums;

internal static class EventSubMessageTypes
{
    public const string WELCOME = "session_welcome";
    public const string KEEPALIVE = "session_keepalive";
    public const string NOTIFICATION = "notification";
    public const string RECONNECT = "session_reconnect";
    public const string REVOCATION = "revocation";
}
