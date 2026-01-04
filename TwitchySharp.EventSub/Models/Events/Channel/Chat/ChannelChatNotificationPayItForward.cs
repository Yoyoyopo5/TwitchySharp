namespace TwitchySharp.EventSub.Models.Events.Channel.Chat;

/// <summary>
/// Contains information about a "pay it forward" chat notification.
/// </summary>
public record ChannelChatNotificationPayItForward
{
    /// <summary>
    /// Indicates whether the gifter is anonymous.
    /// </summary>
    public required bool GifterIsAnonymous { get; init; }
    /// <summary>
    /// The id of the user who gifted the subscription.
    /// This is <see langword="null"/> if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? GifterUserId { get; init; }
    /// <summary>
    /// The display name of the user who gifted the subscription.
    /// This is <see langword="null"/> if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? GifterUserName { get; init; }
    /// <summary>
    /// The login (username) of the user who gifted the subscription.
    /// This is <see langword="null"/> if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public string? GifterUserLogin { get; init; }
}
