using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.HypeTrain;

/// <summary>
/// Contains static definitions for possible hype train contribution types.
/// </summary>
/// <param name="Value">The string value of the hype train contribution type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<HypeTrainContributionType, string>))]
public record HypeTrainContributionType(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// Contributed by cheering with bits.
    /// </summary>
    public static HypeTrainContributionType Bits { get; } = new("bits");
    /// <summary>
    /// Contributed by subscribing or gifting subscriptions.
    /// </summary>
    public static HypeTrainContributionType Subs { get; } = new("subs");
    /// <summary>
    /// Contributed in any other way.
    /// </summary>
    public static HypeTrainContributionType Other { get; } = new("other");
}
