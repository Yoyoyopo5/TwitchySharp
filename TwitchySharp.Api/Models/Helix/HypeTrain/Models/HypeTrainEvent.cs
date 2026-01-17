using System;

namespace TwitchySharp.Api.Models.Helix.HypeTrain.Models;

/// <summary>
/// Contains information about a specific Hype Train event.
/// </summary>
public record HypeTrainEvent
{
    /// <summary>
    /// The id of this event.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The type of event.
    /// For this response, the value is always <c>"hypetrain.progression"</c>.
    /// </summary>
    public required string EventType { get; init; }
    /// <summary>
    /// The date and time when the event occurred.
    /// </summary>
    public required DateTimeOffset EventTimestamp { get; init; }
    /// <summary>
    /// The version number of the definition of the event’s data. 
    /// For example, the value is <c>"1"</c> if the data in <see cref="EventData"/> uses the first definition of the event’s data. 
    /// </summary>
    public required string Version { get; init; }
    /// <summary>
    /// The event data.
    /// </summary>
    public required HypeTrainEventData EventData { get; init; }
}
