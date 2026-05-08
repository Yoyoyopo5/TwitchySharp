namespace TwitchySharp.Api.Helix.ChannelPoints;

/// <summary>
/// Controls the settings for how many times per stream a channel point reward can be redeemed.
/// </summary>
public record MaxPerStreamSetting
{
    /// <summary>
    /// Determines whether the reward applies a limit on the number of redemptions allowed per live stream. 
    /// Is <see langword="true"/> if the reward applies a limit.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The maximum number of redemptions allowed per live stream.
    /// </summary>
    public required long MaxPerStream { get; init; }
}
