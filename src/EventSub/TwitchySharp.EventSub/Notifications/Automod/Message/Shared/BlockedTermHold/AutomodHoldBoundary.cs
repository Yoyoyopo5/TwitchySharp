using System.Text.Json.Serialization;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific location in a message that triggered Automod.
/// </summary>
public readonly record struct AutomodHoldBoundary
{
    /// <summary>
    /// Index in the message for the start of the problem (0 indexed, inclusive).
    /// </summary>
    [JsonPropertyName("start_pos")]
    public required int StartPosition { get; init; }
    /// <summary>
    /// Index in the message for the start of the problem (0 indexed, inclusive).
    /// </summary>
    [JsonPropertyName("end_pos")]
    public required int EndPosition { get; init; }
}
