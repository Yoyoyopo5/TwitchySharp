namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a gifted drops summary notification.
/// </summary>
public record ChannelChatNotificationGiftedDropsSummary
{
    /// <summary>
    /// The number of users who received a gifted Drop.
    /// </summary>
    public required int RecipientCount { get; init; }
}
