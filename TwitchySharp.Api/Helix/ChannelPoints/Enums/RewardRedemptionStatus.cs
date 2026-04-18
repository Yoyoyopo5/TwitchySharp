using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.ChannelPoints;

/// <summary>
/// Contains static definitions for possible Channel Points reward redemption statuses.
/// </summary>
/// <param name="Value">The string value of the redemption status.</param>
[Wrapper<string>]
public readonly partial record struct RewardRedemptionStatus(string Value)
{
    public static RewardRedemptionStatus Canceled { get; } = new("CANCELED");
    public static RewardRedemptionStatus Fulfilled { get; } = new("FULFILLED");
    public static RewardRedemptionStatus Unfulfilled { get; } = new("UNFULFILLED");
}
