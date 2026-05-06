namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

/// <summary>
/// Contains information about a chat announcement notification.
/// </summary>
public record ChannelChatNotificationAnnouncement
{
    /// <summary>
    /// The color of the announcement.
    /// </summary>
    public required string Color { get; init; } // Might be optional, need to test.
}
