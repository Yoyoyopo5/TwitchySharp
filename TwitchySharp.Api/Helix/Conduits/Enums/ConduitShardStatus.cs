namespace TwitchySharp.Api.Helix.Conduits;

/// <summary>
/// Represents the status of a conduit shard.
/// </summary>
public enum ConduitShardStatus
{
    /// <summary>
    /// The shard is enabled and working.
    /// </summary>
    Enabled,
    /// <summary>
    /// The shard is pending verification of the specified callback URL.
    /// </summary>
    WebhookCallbackVerificationPending,
    /// <summary>
    /// The specified callback URL failed verification.
    /// </summary>
    WebhookCallbackVerificationFailed,
    /// <summary>
    /// The notification delivery failure rate was too high.
    /// </summary>
    NotificationFailuresExceeded,
    /// <summary>
    /// The client closed the connection.
    /// </summary>
    WebsocketDisconnected,
    /// <summary>
    /// The client failed to respond to a ping message.
    /// </summary>
    WebsocketFailedPingPong,
    /// <summary>
    /// The client sent a non-pong message. 
    /// Clients may only send pong messages (and only in response to a ping message).
    /// </summary>
    WebsocketReceivedInboundTraffic,
    /// <summary>
    /// The Twitch WebSocket server experienced an unexpected error.
    /// </summary>
    WebsocketInternalError,
    /// <summary>
    /// The Twitch WebSocket server timed out writing the message to the client.
    /// </summary>
    WebsocketNetworkTimeout,
    /// <summary>
    /// The Twitch WebSocket server experienced a network error writing the message to the client.
    /// </summary>
    WebsocketNetworkError,
    /// <summary>
    /// The client failed to reconnect to the Twitch WebSocket server within the required time after a Reconnect Message.
    /// </summary>
    WebsocketFailedToReconnect
}
