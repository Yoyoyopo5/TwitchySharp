namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a chat announcement notification.
/// </summary>
public record ChannelChatNotificationAnnouncement
{
    /// <summary>
    /// The color of the announcement.
    /// </summary>
    public required RgbColor Color { get; init; } // Might be optional, need to test.
}
