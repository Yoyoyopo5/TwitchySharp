namespace TwitchySharp.EventSub.Websocket.Messages.Enums;

/// <summary>
/// Constant message type definitions for switching.
/// </summary>
internal static class EventSubMessageTypes
{
    public const string WELCOME = "session_welcome";
    public const string KEEPALIVE = "session_keepalive";
    public const string NOTIFICATION = "notification";
    public const string RECONNECT = "session_reconnect";
    public const string REVOCATION = "revocation";
}
