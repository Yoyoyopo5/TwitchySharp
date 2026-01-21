using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch Channel Points reward.
/// </summary>
/// <param name="Value">The string value of the reward id.</param>
[JsonConverter(typeof(WrapperJsonConverter<RewardId, string>))]
public readonly record struct RewardId(string Value) : IWrapValue<string>
{
    public static implicit operator string(RewardId id)
        => id.Value;
    public override string ToString()
        => Value;
}
