namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a gifted subscription that appeared in a chat notification.
/// </summary>
public record ChannelChatNotificationGiftedSubscription
{
    /// <summary>
    /// The number of months the subscription is for.
    /// </summary>
    public required int DurationMonths { get; init; }
    /// <summary>
    /// The total amount of gifted subscriptions the gifter has given in the channel.
    /// This is <see langword="null"/> if the gifter is anonymous.
    /// </summary>
    public int? CumulativeTotal { get; init; }
    /// <summary>
    /// The id of the user that received the gifted subscription.
    /// </summary>
    public required UserId RecipientUserId { get; init; }
    /// <summary>
    /// The display name of the user that received the gifted subscription.
    /// </summary>
    public required UserName RecipientUserName { get; init; }
    /// <summary>
    /// The login (username) of the user that received the gifted subscription.
    /// </summary>
    public required UserLogin RecipientUserLogin { get; init; }
    /// <summary>
    /// The tier of the gifted subscription.
    /// </summary>
    public required SubscriptionTier SubTier { get; init; }
    /// <summary>
    /// The id of the associated community gift event.
    /// This is <see langword="null"/> if the gifted subscription is not part of a community gift.
    /// </summary>
    public CommunityGiftEventId? CommunityGiftId { get; init; }
}
