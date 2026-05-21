namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a gifted subscription paid upgrade that appeared in a chat notification.
/// </summary>
public record ChannelChatNotificationGiftedSubscriptionPaidUpgrade
{
    /// <summary>
    /// Indicates whether the gifter is anonymous.
    /// </summary>
    public required bool GifterIsAnonymous { get; init; }
    /// <summary>
    /// The id of the user that gifted the subscription.
    /// This is <see langword="null"/> if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public UserId? GifterUserId { get; init; }
    /// <summary>
    /// The display name of the user that gifted the subscription.
    /// This is <see langword="null"/> if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public UserName? GifterUserName { get; init; }
    // Strange, docs don't indicate GifterUserLogin, but surely its here.
    /// <summary>
    /// The login (username) of the user that gifted the subscription.
    /// This is <see langword="null"/> if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public UserLogin? GifterUserLogin { get; init; }
}
