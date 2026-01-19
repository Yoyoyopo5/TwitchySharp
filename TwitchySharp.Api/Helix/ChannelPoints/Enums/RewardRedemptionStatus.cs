using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.ChannelPoints;

/// <summary>
/// Contains static definitions for possible Channel Points reward redemption statuses.
/// </summary>
/// <param name="Value">The string value of the redemption status.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<RewardRedemptionStatus, string>))]
public record RewardRedemptionStatus(string Value) : ValueBackedEnum<string>(Value)
{
    public static RewardRedemptionStatus Cancelled { get; } = new("cancelled");
    public static RewardRedemptionStatus Fulfilled { get; } = new("fulfilled");
    public static RewardRedemptionStatus Unfulfilled { get; } = new("unfulfilled");
}
