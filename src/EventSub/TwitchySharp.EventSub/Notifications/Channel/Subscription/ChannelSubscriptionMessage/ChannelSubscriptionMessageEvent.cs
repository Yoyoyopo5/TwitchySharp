namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSubscriptionMessage"/> event.
/// </summary>
public record ChannelSubscriptionMessageEvent
{
    /// <summary>
    /// The id of the user that sent the resubscription chat message.
    /// </summary>
    public UserId? UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that sent the resubscription chat message.
    /// </summary>
    public UserLogin? UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that sent the resubscription chat message.
    /// </summary>
    public UserName? UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the resubscription was made to.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the resubscription was made to.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the resubscription was made to.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The tier of the subscription.
    /// </summary>
    public required SubscriptionTier Tier { get; init; }
    /// <summary>
    /// The resubscription message that the user sent in chat.
    /// </summary>
    public required ResubscriptionMessage Message { get; init; }
    /// <summary>
    /// The total number of months the user has been subscribed to the channel.
    /// </summary>
    public required int CumulativeMonths { get; init; }
    /// <summary>
    /// The number of consecutive months the user has been subscribed to the channel.
    /// This can be <see langword="null"/> if the user has opted out of sharing this information.
    /// </summary>
    public int? StreakMonths { get; init; }
    /// <summary>
    /// The amount of months the resubscription is for.
    /// </summary>
    /// <remarks>
    /// Dev Note: I'm not sure how this property works, as multi-month subscription resubscribe messages are
    /// just for that month. So this could always be <c>1</c>, or it might be the remaining duration of the
    /// original subscription, or even the duration of the original subscription itself.
    /// </remarks>
    public int DurationMonths { get; init; }
}
