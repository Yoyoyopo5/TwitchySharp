using System.Text.Json.Serialization;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.EventSub.Enums.Events.Channel;

namespace TwitchySharp.EventSub.Models.Events.Channel;

/// <summary>
/// Contains information about a specific <see cref="ChannelModerateActionType.FollowersOnlyModeOn"/> action.
/// </summary>
public record ChannelModerateFollowersModeAction
{
    /// <summary>
    /// The length of time that followers must have followed the broadcaster to send messages in chat.
    /// </summary>
    [JsonConverter(typeof(MinutesTimeSpanJsonConverter))]
    [JsonPropertyName("follow_duration_minutes")]
    public required TimeSpan FollowDuration { get; init; }
}
