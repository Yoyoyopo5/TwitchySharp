using TwitchySharp.Shared.Enums;

namespace TwitchySharp.Api.Helix.Subscriptions;

/// <summary>
/// Contains information about a specific user's subscription.
/// </summary>
public record UserSubscription
{
    /// <inheritdoc cref="BroadcasterSubscriber.BroadcasterId"/>
    public required string BroadcasterId { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.BroadcasterLogin"/>
    public required string BroadcasterLogin { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.BroadcasterName"/>
    public required string BroadcasterName { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.GifterId"/>
    public string? GifterId { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.GifterLogin"/>
    public string? GifterLogin { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.GifterName"/>
    public string? GifterName { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.IsGift"/>
    public required bool IsGift { get; init; }
    /// <inheritdoc cref="BroadcasterSubscriber.Tier"/>
    public required SubscriptionTier Tier { get; init; }
}
