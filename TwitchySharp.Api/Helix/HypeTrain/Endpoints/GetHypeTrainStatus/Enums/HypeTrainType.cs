using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.HypeTrain;

/// <summary>
/// Contains static definitions for possible Hype Train types.
/// </summary>
/// <param name="Value">The string value of the Hype Train type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<HypeTrainType, string>))]
public record HypeTrainType(string Value) : ValueBackedEnum<string>(Value)
{
    public static HypeTrainType Treasure { get; } = new("treasure");
    public static HypeTrainType GoldenKappa { get; } = new("golden_kappa");
    public static HypeTrainType Regular { get; } = new("regular");
}
