namespace TwitchySharp.Api.Helix.Bits;

/// <summary>
/// Custom Bits power-up setting for maximum redemptions per user per stream.
/// </summary>
public readonly record struct MaxPerUserPerStreamSetting
{
    /// <summary>
    /// Indicates whether the setting is enabled.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The maximum amount of redemptions a user can make per stream.
    /// </summary>
    public required long MaxPerUserPerStream { get; init; }
}
