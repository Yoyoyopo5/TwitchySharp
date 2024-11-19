using System;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Conduits;

/// <summary>
/// Contains data about a conduit shard's transport mechanism.
/// </summary>
public record ConduitTransport
{
    /// <summary>
    /// The transport method.
    /// Shards can either send events over Webhooks or WebSocket connections.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseLowerJsonStringEnumConverter<ConduitTransportMethod>))]
    public required ConduitTransportMethod Method { get; init; }
    /// <summary>
    /// The callback URL where the notifications are sent. 
    /// Included only if <see cref="Method"/> is set to <see cref="ConduitTransportMethod.Webhook"/>.
    /// </summary>
    public string? Callback { get; init; }
    /// <summary>
    /// An ID that identifies the WebSocket that notifications are sent to.
    /// Included only if <see cref="Method"/> is set to <see cref="ConduitTransportMethod.Websocket"/>.
    /// </summary>
    public string? SessionId { get; init; }
    /// <summary>
    /// The date and time that the WebSocket connection was established.
    /// Included only if <see cref="Method"/> is set to <see cref="ConduitTransportMethod.Websocket"/>.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? ConnectedAt { get; init; }
    /// <summary>
    /// The date and time that the WebSocket connection was lost.
    /// Included only if <see cref="Method"/> is set to <see cref="ConduitTransportMethod.Websocket"/>.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? DisconnectedAt { get; init; }
}
