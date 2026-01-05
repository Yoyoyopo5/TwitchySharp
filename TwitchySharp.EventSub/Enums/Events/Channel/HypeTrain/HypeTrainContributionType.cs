using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.EventSub.Enums.Events.Channel.HypeTrain;

/// <summary>
/// Contains static definitions for possible Hype Train contribution types.
/// </summary>
/// <param name="Value">The string value of the contribution type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<HypeTrainContributionType, string>))]
public record HypeTrainContributionType(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// Bits contributions with Cheering, Power-ups, and Extensions. 
    /// </summary>
    public static HypeTrainContributionType Bits { get; } = new("bits");
    /// <summary>
    /// Subscription activity like subscribing or gifting subscriptions. 
    /// </summary>
    public static HypeTrainContributionType Subscription { get; } = new("subscription");
    /// <summary>
    /// Covers other contribution methods not listed.
    /// </summary>
    public static HypeTrainContributionType Other { get; } = new("other");
}
