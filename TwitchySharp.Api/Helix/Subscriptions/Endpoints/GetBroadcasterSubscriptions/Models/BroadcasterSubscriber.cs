using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Subscriptions;

/// <summary>
/// Contains information about a specific channel subscriber.
/// </summary>
public record BroadcasterSubscriber
{
    /// <summary>
    /// The user id of the broadcaster the subscription is for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster the subscription is for.
    /// </summary>
    public required UserLogin BroadcasterLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster the subscription is for.
    /// </summary>
    public required UserName BroadcasterName { get; init; }
    /// <summary>
    /// The id of the user who gifted the subscription, if the subscription was gifted.
    /// </summary>
    public required UserId GifterId { get; init; }
    /// <summary>
    /// The login (username) of the user who gifted the subscription, if the subscription was gifted.
    /// </summary>
    public required UserLogin GifterLogin { get; init; }
    /// <summary>
    /// The display name of the user who gifted the subscription, if the subscription was gifted.
    /// </summary>
    public required UserName GifterName { get; init; }
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
    public required UserId UserId { get; init; }
    /// <summary>
    /// The display name of the subscriber.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The login (username) of the subscriber.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
}
