namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific channel points reward max per user per stream setting.
/// </summary>
public record MaxPerUserPerStreamSetting
{
    /// <summary>
    /// Indicates whether the max per user per stream setting is enabled.
    /// </summary>
    public required bool IsEnabled { get; init; }
    /// <summary>
    /// The maximum per user per stream limit.
    /// </summary>
    public required int Value { get; init; }
}
