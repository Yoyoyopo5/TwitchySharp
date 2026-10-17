namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a modiversary event.
/// </summary>
public record ChannelChatNotificationModiversary
{
    /// <summary>
    /// The number of months the user has been a moderator in the channel.
    /// </summary>
    public required int Months { get; init; }
}
