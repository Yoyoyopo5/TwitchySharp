using System.Text.Json.Serialization;
using TwitchySharp.EventSub.Enums.Events.Channel;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.EventSub.Models.Events.Channel;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.SlowModeOn"/> action.
/// </summary>
public record ChannelModerateSlowModeAction
{
    /// <summary>
    /// The amount of time that users need to wait between sending messages in chat.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    [JsonPropertyName("wait_time_seconds")]
    public required TimeSpan WaitTime { get; init; }
}
