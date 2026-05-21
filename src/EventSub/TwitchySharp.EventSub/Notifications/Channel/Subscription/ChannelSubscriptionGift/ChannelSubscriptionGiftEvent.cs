namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSubscriptionGift"/> event.
/// </summary>
public record ChannelSubscriptionGiftEvent
{
    /// <summary>
    /// The id of the user that sent the subscription gift.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public UserId? UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the subscription gift.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public UserLogin? UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that sent the subscription gift.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public UserName? UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the subscription was made to.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the subscription was made to.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the subscription was made to.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The number of subscriptions in the subscription gift.
    /// </summary>
    public required int Total { get; init; }
    /// <summary>
    /// The tier of the subscription gift.
    /// </summary>
    public required SubscriptionTier Tier { get; init; }
    /// <summary>
    /// The total number of subscriptions gifted by the user in this channel.
    /// This is <see langword="null"/> if <see cref="IsAnonymous"/> is <see langword="true"/>.
    /// </summary>
    public int? CumulativeTotal { get; init; }
    /// <summary>
    /// Indicates whether the subscription was gifted anonymously.
    /// </summary>
    public required bool IsAnonymous { get; init; }
}
