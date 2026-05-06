using System.Text.Json.Serialization;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.EventSub.Models.Events.Channel.ChannelPoints;

/// <summary>
/// Contains information about a specific channel point reward's global cooldown setting.
/// </summary>
public record GlobalCooldownSetting : ISetting<TimeSpan>
{
    /// <summary>
    /// Indicates whether global cooldown is enabled for the reward.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The duration of the cooldown.
    /// This amount of time must elapse after a redemption before the reward can be redeemed again by any user.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    [JsonPropertyName("seconds")]
    public required TimeSpan Duration { get; init; }
    TimeSpan ISetting<TimeSpan>.Value => Duration;
}
