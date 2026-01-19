using TwitchySharp.Shared.Enums;

namespace TwitchySharp.Api.Helix.Subscriptions;

/// <summary>
/// Contains information about a specific channel subscriber.
/// </summary>
public record BroadcasterSubscriber
{
    /// <summary>
    /// The user id of the broadcaster the subscription is for.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster the subscription is for.
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster the subscription is for.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The id of the user who gifted the subscription, if the subscription was gifted.
    /// </summary>
    public required string GifterId { get; init; }
    /// <summary>
    /// The login (username) of the user who gifted the subscription, if the subscription was gifted.
    /// </summary>
    public required string GifterLogin { get; init; }
    /// <summary>
    /// The display name of the user who gifted the subscription, if the subscription was gifted.
    /// </summary>
    public required string GifterName { get; init; }
    /// <summary>
    /// Indicates whether the subscription was gifted.
    /// </summary>
    public required bool IsGift { get; init; }
    /// <summary>
    /// The name of the subscription tier as defined by the broadcaster.
    /// </summary>
    public required string PlanName { get; init; }
    /// <summary>
    /// The tier of the subscription.
    /// </summary>
    public required SubscriptionTier Tier { get; init; }
    /// <summary>
    /// The user id of the subscriber.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the subscriber.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The login (username) of the subscriber.
    /// </summary>
    public required string UserLogin { get; init; }
}
