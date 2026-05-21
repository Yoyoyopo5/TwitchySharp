using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Notifications;

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
