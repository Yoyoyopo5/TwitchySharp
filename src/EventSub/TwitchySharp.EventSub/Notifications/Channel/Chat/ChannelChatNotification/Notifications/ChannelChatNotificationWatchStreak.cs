namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a watch streak notification that appeared in a chat notification.
/// </summary>
public record ChannelChatNotificationWatchStreak
{
    /// <summary>
    /// The number of consecutive broadcasts for which the user has been watching.
    /// </summary>
    public required int StreakCount { get; init; }
    /// <summary>
    /// The number of channel points awarded for the Watch Streak milestone.
    /// </summary>
    public required int ChannelPointsAwarded { get; init; }
}
