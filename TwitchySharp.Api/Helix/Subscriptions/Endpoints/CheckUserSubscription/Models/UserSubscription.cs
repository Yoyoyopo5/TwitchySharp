using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Subscriptions;

/// <summary>
/// Contains information about a specific user's subscription.
/// </summary>
public record UserSubscription
{
    /// <inheritdoc cref="BroadcasterSubscriber.BroadcasterId"/>
    public required UserId BroadcasterId { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.BroadcasterLogin"/>
    public required UserLogin BroadcasterLogin { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.BroadcasterName"/>
    public required UserName BroadcasterName { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.GifterId"/>
    public UserId? GifterId { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.GifterLogin"/>
    public UserLogin? GifterLogin { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.GifterName"/>
    public UserName? GifterName { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.IsGift"/>
    public required bool IsGift { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.Tier"/>
    public required SubscriptionTier Tier { get; init; }
}
