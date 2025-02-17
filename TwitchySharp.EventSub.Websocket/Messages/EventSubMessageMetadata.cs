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

internal static class EventSubMessageType
{
    public const string Welcome = "welcome";
    public const string Keepalive = "keepalive";
    public const string Notification = "notification";
    public const string Reconnect = "reconnect";
    public const string Revocation = "revocation";
}
