namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a channel resubscription that appeared in a chat notification.
/// </summary>
public record ChannelChatNotificationResubscription
{
    /// <summary>
    /// The total number of months the user has been subscribed to the channel.
    /// </summary>
    public required int CumulativeMonths { get; init; }
    /// <summary>
    /// The number of months the resubscription is for.
    /// </summary>
    public required int DurationMonths { get; init; }
    /// <summary>
    /// The number of consecutive months the user has been subscribed to the channel.
    /// </summary>
    public required int StreakMonths { get; init; }
    /// <summary>
    /// The tier of the subscription.
    /// </summary>
    public required SubscriptionTier SubTier { get; init; }
    /// <summary>
    /// Indicates if the subscription was obtained through Amazon Prime.
    /// </summary>
    public bool? IsPrime { get; init; } // Marked optional in documentation, no idea why. Docs also fucked here, so we'll have to figure it out live.
    /// <summary>
    /// Indicates if the resubscription is the result of a gift.
    /// </summary>
    public bool IsGift { get; init; }
    /// <summary>
    /// Indicates if the resubscription gifter is anonymous.
    /// Is <see langword="null"/> if <see cref="IsGift"/> is <see langword="false"/>.
    /// </summary>
    public bool? GifterIsAnonymous { get; init; }
    /// <summary>
    /// The id of the user that gifted the subscription.
    /// Is <see langword="null"/> if <see cref="IsGift"/> is <see langword="false"/>, or if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public UserId? GifterUserId { get; init; }
    /// <summary>
    /// The display name of the user that gifted the subscription.
    /// Is <see langword="null"/> if <see cref="IsGift"/> is <see langword="false"/>, or if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public UserName? GifterUserName { get; init; }
    /// <summary>
    /// The login (username) of the user that gifted the subscription.
    /// Is <see langword="null"/> if <see cref="IsGift"/> is <see langword="false"/>, or if <see cref="GifterIsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public UserLogin? GifterUserLogin { get; init; }
}
