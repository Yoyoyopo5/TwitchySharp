using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// Contains static definitions for various EventSub subscription states.
/// </summary>
/// <param name="Value"></param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<EventSubSubscriptionStatus, string>))]
public record EventSubSubscriptionStatus(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// The subscription is enabled.
    /// </summary>
    public static EventSubSubscriptionStatus Enabled { get; } = new("enabled");
    /// <summary>
    /// The subscription is pending verification of the specified callback URL.
    /// </summary>
    public static EventSubSubscriptionStatus WebhookCallbackVerificationPending { get; } = new("webhook_callback_verification_pending");
    /// <summary>
    /// The specified callback URL failed verification.
    /// </summary>
    public static EventSubSubscriptionStatus WebhookCallbackVerificationFailed { get; } = new("webhook_callback_verification_failed");
    /// <summary>
    /// The notification delivery failure rate was too high.
    /// </summary>
    public static EventSubSubscriptionStatus NotificationFailuresExceeded { get; } = new("notification_failures_exceeded");
    /// <summary>
    /// The authorization was revoked for one or more users specified in the Condition object.
    /// </summary>
    public static EventSubSubscriptionStatus AuthorizationRevoked { get; } = new("authorization_revoked");
    /// <summary>
    /// The moderator that authorized the subscription is no longer one of the broadcaster's moderators.
    /// </summary>
    public static EventSubSubscriptionStatus ModeratorRemoved { get; } = new("moderator_removed");
    /// <summary>
    /// One of the users specified in the Condition object was removed.
    /// </summary>
    public static EventSubSubscriptionStatus UserRemoved { get; } = new("user_removed");
    /// <summary>
    /// The user specified in the Condition object was banned from the broadcaster's chat.
    /// </summary>
    public static EventSubSubscriptionStatus ChatUserBanned { get; } = new("chat_user_banned");
    /// <summary>
    /// The subscription to subscription type and version is no longer supported.
    /// </summary>
    public static EventSubSubscriptionStatus VersionRemoved { get; } = new("version_removed");
    /// <summary>
    /// The subscription to the beta subscription type was removed due to maintenance.
    /// </summary>
    public static EventSubSubscriptionStatus BetaMaintenance { get; } = new("beta_maintenance");
    /// <summary>
    /// The client closed the connection.
    /// </summary>
    public static EventSubSubscriptionStatus WebsocketDisconnected { get; } = new("websocket_disconnected");
    /// <summary>
    /// The client failed to respond to a ping message.
    /// </summary>
    public static EventSubSubscriptionStatus WebsocketFailedPingPong { get; } = new("websocket_failed_ping_pong");
    /// <summary>
    /// The client sent a non-pong message. Clients may only send pong messages (and only in response to a ping message).
    /// </summary>
    public static EventSubSubscriptionStatus WebsocketReceivedInboundTraffic { get; } = new("websocket_received_inbound_traffic");
    /// <summary>
    /// The client failed to subscribe to events within the required time.
    /// </summary>
    public static EventSubSubscriptionStatus WebsocketConnectionUnused { get; } = new("websocket_connection_unused");
    /// <summary>
    /// The Twitch WebSocket server experienced an unexpected error.
    /// </summary>
    public static EventSubSubscriptionStatus WebsocketInternalError { get; } = new("websocket_internal_error");
    /// <summary>
    /// The Twitch WebSocket server timed out writing the message to the client.
    /// </summary>
    public static EventSubSubscriptionStatus WebsocketNetworkTimeout { get; } = new("websocket_network_timeout");
    /// <summary>
    /// The Twitch WebSocket server experienced a network error writing the message to the client.
    /// </summary>
    public static EventSubSubscriptionStatus WebsocketNetworkError { get; } = new("websocket_network_error");
    /// <summary>
    /// The client failed to reconnect to the Twitch WebSocket server within the required time after a Reconnect Message.
    /// </summary>
    public static EventSubSubscriptionStatus WebsocketFailedToReconnect { get; } = new("websocket_failed_to_reconnect");
}
