using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific channel point reward's global cooldown setting.
/// </summary>
public record GlobalCooldownSetting
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
}
