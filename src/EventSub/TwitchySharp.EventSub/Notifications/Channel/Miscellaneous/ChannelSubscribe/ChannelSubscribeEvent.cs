namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSubscribe"/> event.
/// </summary>
public record ChannelSubscribeEvent
{
    /// <summary>
    /// The id of the user that subscribed.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that subscribed.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that subscribed.
    /// </summary>
    public required UserName UserName { get; init; }
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
    /// The tier of the subscription.
    /// </summary>
    public required SubscriptionTier Tier { get; init; }
    /// <summary>
    /// Indicates whether the subscription is a gift.
    /// </summary>
    public required bool IsGift { get; init; }
}
