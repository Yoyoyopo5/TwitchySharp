using System;

namespace TwitchySharp.Api.Helix.Conduits;

/// <summary>
/// Contains information used to update the transport mechanism of a specific shard.
/// Used derived classes <see cref="ConduitWebhookTransportUpdate"/> and <see cref="ConduitWebsocketTransportUpdate"/>.
/// </summary>
public abstract record ConduitTransportUpdate
{
    /// <summary>
    /// The method to use for the transport.
    /// </summary>
    public ConduitTransportMethod? Method { get; protected init; }
    /// <summary>
    /// The callback url where webhook notifications are sent.
    /// The URL must use the HTTPS protocol and port 443.
    /// <b>Note:</b> Redirects are not followed.
    /// </summary>
    public Uri? Callback { get; protected init; }
    /// <summary>
    /// The secret used to verify the signature of a webhook notification.
    /// The secret must be an ASCII string that’s a minimum of 10 characters long and a maximum of 100 characters long.
    /// </summary>
    public string? Secret { get; protected init; }
    /// <summary>
    /// The id of the WebSocket connection to send notifications to.
    /// When you connect to EventSub using WebSockets, the server returns this id in the Welcome message.
    /// </summary>
    public EventSubWebsocketSessionId? SessionId { get; protected init; }
}
