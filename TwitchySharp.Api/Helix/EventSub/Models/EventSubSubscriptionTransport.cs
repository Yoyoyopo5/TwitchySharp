using System;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// Contains data about the transport method of an EventSub subscription.
/// </summary>
public record EventSubSubscriptionTransport
{
    /// <summary>
    /// The transport method type.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseLowerJsonStringEnumConverter<EventSubTransportMethod>))]
    public required EventSubTransportMethod Method { get; init; }
    /// <summary>
    /// The callback URL where the notifications are sent. 
    /// Included only if method is set to <see cref="EventSubTransportMethod.Webhook"/>.
    /// </summary>
    public string? Callback { get; init; }
    /// <summary>
    /// An id that identifies the WebSocket that notifications are sent to. 
    /// Included only if method is set to <see cref="EventSubTransportMethod.Websocket"/>.
    /// </summary>
    public string? SessionId { get; init; }
    /// <summary>
    /// The date and time that the WebSocket connection was established. 
    /// Included only if method is set to <see cref="EventSubTransportMethod.Websocket"/>.
    /// </summary>
    public DateTimeOffset? ConnectedAt { get; init; }
    /// <summary>
    /// An id that identifies the conduit to send notifications to. 
    /// Included only if method is set to <see cref="EventSubTransportMethod.Conduit"/>.
    /// </summary>
    public string? ConduitId { get; init; }
}
