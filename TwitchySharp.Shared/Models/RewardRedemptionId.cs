using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Channel Points reward redemption.
/// </summary>
/// <param name="Value">The string value of the redemption id.</param>
[JsonConverter(typeof(WrapperJsonConverter<RewardRedemptionId, string>))]
public readonly record struct RewardRedemptionId(string Value) : IWrapValue<string>
{
    public static implicit operator string(RewardRedemptionId id)
        => id.Value;
    public override string ToString()
        => Value;
}