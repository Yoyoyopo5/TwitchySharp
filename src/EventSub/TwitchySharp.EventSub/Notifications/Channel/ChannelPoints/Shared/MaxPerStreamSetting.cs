namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific channel points reward max per stream setting.
/// </summary>
public record MaxPerStreamSetting
{
    /// <summary>
    /// Indicates whether the max per stream setting is enabled.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The maximum per stream limit.
    /// </summary>
    public required int Value { get; init; }
}
