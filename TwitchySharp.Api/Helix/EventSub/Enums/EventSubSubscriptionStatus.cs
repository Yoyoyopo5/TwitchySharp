namespace TwitchySharp.Api.Helix.EventSub;

public enum EventSubSubscriptionStatus
{
    /// <summary>
    /// The subscription is enabled.
    /// </summary>
    Enabled,
    /// <summary>
    /// The subscription is pending verification of the specified callback URL.
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
    /// The authorization was revoked for one or more users specified in the Condition object.
    /// </summary>
    AuthorizationRevoked,
    /// <summary>
    /// The moderator that authorized the subscription is no longer one of the broadcaster's moderators.
    /// </summary>
    ModeratorRemoved,
    /// <summary>
    /// One of the users specified in the Condition object was removed.
    /// </summary>
    UserRemoved,
    /// <summary>
    /// The user specified in the Condition object was banned from the broadcaster's chat.
    /// </summary>
    ChatUserBanned,
    /// <summary>
    /// The subscription to subscription type and version is no longer supported.
    /// </summary>
    VersionRemoved,
    /// <summary>
    /// The subscription to the beta subscription type was removed due to maintenance.
    /// </summary>
    BetaMaintenance,
    /// <summary>
    /// The client closed the connection.
    /// </summary>
    WebsocketDisconnected,
    /// <summary>
    /// The client failed to respond to a ping message.
    /// </summary>
    WebsocketFailedPingPong,
    /// <summary>
    /// The client sent a non-pong message. Clients may only send pong messages (and only in response to a ping message).
    /// </summary>
    WebsocketReceivedInboundTraffic,
    /// <summary>
    /// The client failed to subscribe to events within the required time.
    /// </summary>
    WebsocketConnectionUnused,
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
