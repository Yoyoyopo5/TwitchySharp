namespace TwitchySharp.Api.Helix.Bits;

/// <summary>
/// Custom Bits power-up setting for maximum redemptions per stream.
/// </summary>
public readonly record struct MaxPerStreamSetting
{
    /// <summary>
    /// Indicates whether the setting is enabled.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The maximum amount of redemptions that can be made per stream.
    /// </summary>
    public required long MaxPerStream { get; init; }
}
