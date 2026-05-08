using System;
using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.ChannelPoints;

/// <summary>
/// Controls the setting for cooldown on a channel point reward.
/// </summary>
public record GlobalCooldownSetting
{
    /// <summary>
    /// Determines whether to apply a cooldown period. Is <see langword="true"/> if a cooldown period is enabled.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The cooldown period.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    [JsonPropertyName("global_cooldown_seconds")]
    public required TimeSpan GlobalCooldown { get; init; }
}
