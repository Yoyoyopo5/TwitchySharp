namespace TwitchySharp.Api.Models.Helix.ChannelPoints.Models;

/// <summary>
/// Controls the setting for how many time per stream an individual user can redeem a channel point reward.
/// </summary>
public record MaxPerUserPerStreamRewardSetting
{
    /// <summary>
    /// Determines whether the reward applies a limit on the number of redemptions allowed per user per live stream. 
    /// Is <see langword="true"/> if the reward applies a limit.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The maximum number of redemptions allowed per user per live stream.
    /// </summary>
    public required long MaxPerUserPerStream { get; init; }
}
