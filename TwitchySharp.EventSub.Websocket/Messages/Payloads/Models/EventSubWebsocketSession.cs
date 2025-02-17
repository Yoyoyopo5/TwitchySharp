using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.EventSub.Websocket.Messages.Payloads;

/// <summary>
/// Contains details about an EventSub websocket session.
/// </summary>
public record EventSubWebsocketSession
{
    /// <summary>
    /// The id of the session.
    /// Use this to create and update EventSub subscriptions that you want to notify to this session.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The status of the session.
    /// </summary>
    public required EventSubSessionStatus Status { get; init; }
    /// <summary>
    /// The amount of time (seconds) that you should expect silence before receiving a keepalive message. 
    /// For a welcome message, this is also the amount of time that you have to subscribe to an event after receiving the welcome message. 
    /// If you don’t subscribe to an event within this window, the session is disconnected.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter)), JsonPropertyName("keepalive_timeout_seconds")]
    public required TimeSpan KeepaliveTimeout { get; init; }
    /// <summary>
    /// The date and time when the session was connected.
    /// </summary>
    public DateTimeOffset ConnectedAt { get; init; }
}
