using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Subscriptions;
/// <inheritdoc cref="UserSubscription"/>
public record CheckUserSubscriptionResponse
{
    /// <summary>
    /// A list containing a single object with information about the user's subscription.
    /// </summary>
    public required UserSubscription[] Data { get; init; }
}

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
    [JsonConverter(typeof(SubscriptionTierJsonConverter))]
    public required SubscriptionTier Tier { get; init; }
}
