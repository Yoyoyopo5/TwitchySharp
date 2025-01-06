using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace TwitchySharp.Api.Helix.Subscriptions;

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
