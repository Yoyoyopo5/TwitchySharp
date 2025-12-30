using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Enums;
/// <summary>
/// Contains static definitions for Twitch subscription tiers.
/// </summary>
/// <param name="Value">The string value of the subscription tier.</param>

[JsonConverter(typeof(ValueBackedEnumJsonConverter<SubscriptionTier, string>))]
public record SubscriptionTier(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// First level of paid or Prime subscription.
    /// </summary>
    public static SubscriptionTier Tier1 { get; } = new("1000");
    /// <summary>
    /// Second level of paid subscription.
    /// </summary>
    public static SubscriptionTier Tier2 { get; } = new("2000");
    /// <summary>
    /// Third level of paid subscription.
    /// </summary>
    public static SubscriptionTier Tier3 { get; } = new("3000");
}
