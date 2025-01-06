using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Subscriptions;
/// <summary>
/// Contains a list of a specific broadcaster's subscribers.
/// </summary>
public record GetBroadcasterSubscriptionsResponse
{
    /// <summary>
    /// The list of subscribers.
    /// </summary>
    public required BroadcasterSubscriber[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
    /// <summary>
    /// The total number of users that subscribe to this broadcaster.
    /// </summary>
    public required int Total { get; init; }
    /// <summary>
    /// The current number of subscriber points earned by this broadcaster. 
    /// Points are based on the subscription tier of each user that subscribes to this broadcaster. 
    /// For example, a Tier 1 subscription is worth 1 point, Tier 2 is worth 2 points, and Tier 3 is worth 6 points.
    /// </summary>
    public required int Points { get; init; }
}

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
    [JsonConverter(typeof(SubscriptionTierJsonConverter))]
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

/// <summary>
/// Available subscription tiers.
/// </summary>
public enum SubscriptionTier
{
    Tier1,
    Tier2,
    Tier3
}

public class SubscriptionTierJsonConverter : JsonConverter<SubscriptionTier>
{
    private const string TIER_1 = "1000";
    private const string TIER_2 = "2000";
    private const string TIER_3 = "3000";

    public override SubscriptionTier Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.GetString() switch
        {
            TIER_1 => SubscriptionTier.Tier1,
            TIER_2 => SubscriptionTier.Tier2,
            TIER_3 => SubscriptionTier.Tier3,
            _ => throw new NotSupportedException()
        };

    public override void Write(Utf8JsonWriter writer, SubscriptionTier value, JsonSerializerOptions options)
        => writer.WriteStringValue(value switch 
            { 
                SubscriptionTier.Tier1 => TIER_1,
                SubscriptionTier.Tier2 => TIER_2,
                SubscriptionTier.Tier3 => TIER_3,
                _ => throw new NotSupportedException()
            });
}
