using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.Bits;

/// <summary>
/// Custom Bits power-up setting for global redemption cooldowns.
/// </summary>
public readonly record struct GlobalCooldownSetting
{
    /// <summary>
    /// Indicates whether the setting is enabled.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The amount of time that must elapse bewteen redemptions.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    [JsonPropertyName("global_cooldown_seconds")]
    public required TimeSpan GlobalCooldown { get; init; }
}
