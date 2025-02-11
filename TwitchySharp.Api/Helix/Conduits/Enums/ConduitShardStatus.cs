using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Conduits;

/// <summary>
/// Contains static definitions that represent the status of a conduit shard.
/// </summary>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ConduitShardStatus, string>))]
public record ConduitShardStatus(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// The shard is enabled and working.
    /// </summary>
    public static ConduitShardStatus Enabled { get; } = new("enabled");
    /// <summary>
    /// The shard is pending verification of the specified callback URL.
    /// </summary>
    public static ConduitShardStatus WebhookCallbackVerificationPending { get; } = new("webhook_callback_verification_pending");
    /// <summary>
    /// The specified callback URL failed verification.
    /// </summary>
    public static ConduitShardStatus WebhookCallbackVerificationFailed { get; } = new("webhook_callback_verification_failed");
    /// <summary>
    /// The notification delivery failure rate was too high.
    /// </summary>
    public static ConduitShardStatus NotificationFailuresExceeded { get; } = new("notification_failures_exceeded");
    /// <summary>
    /// The client closed the connection.
    /// </summary>
    public static ConduitShardStatus WebsocketDisconnected { get; } = new("websocket_disconnected");
    /// <summary>
    /// The client failed to respond to a ping message.
    /// </summary>
    public static ConduitShardStatus WebsocketFailedPingPong { get; } = new("websocket_failed_ping_pong");
    /// <summary>
    /// The client sent a non-pong message. 
    /// Clients may only send pong messages (and only in response to a ping message).
    /// </summary>
    public static ConduitShardStatus WebsocketReceivedInboundTraffic { get; } = new("websocket_received_inbound_traffic");
    /// <summary>
    /// The Twitch WebSocket server experienced an unexpected error.
    /// </summary>
    public static ConduitShardStatus WebsocketInternalError { get; } = new("websocket_internal_error");
    /// <summary>
    /// The Twitch WebSocket server timed out writing the message to the client.
    /// </summary>
    public static ConduitShardStatus WebsocketNetworkTimeout { get; } = new("websocket_network_timeout");
    /// <summary>
    /// The Twitch WebSocket server experienced a network error writing the message to the client.
    /// </summary>
    public static ConduitShardStatus WebsocketNetworkError { get; } = new("websocket_network_error");
    /// <summary>
    /// The client failed to reconnect to the Twitch WebSocket server within the required time after a Reconnect Message.
    /// </summary>
    public static ConduitShardStatus WebsocketFailedToReconnect { get; } = new("websocket_failed_to_reconnect");
}
