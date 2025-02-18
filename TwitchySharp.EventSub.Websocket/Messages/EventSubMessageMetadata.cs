using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Websocket.Messages;

internal record EventSubMessageMetadata
{
    public required string MessageId { get; init; }
    public required string MessageType { get; init; }
    public required DateTimeOffset MessageTimestamp { get; init; }
    public string? SubscriptionType { get; init; }
    public string? SubscriptionVersion { get; init; }
}

internal static class EventSubMessageTypes
{
    public const string WELCOME = "session_welcome";
    public const string KEEPALIVE = "session_keepalive";
    public const string NOTIFICATION = "notification";
    public const string RECONNECT = "session_reconnect";
    public const string REVOCATION = "revocation";
}
