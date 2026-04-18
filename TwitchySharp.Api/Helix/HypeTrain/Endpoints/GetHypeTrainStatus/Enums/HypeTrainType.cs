using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.HypeTrain;

/// <summary>
/// Contains static definitions for possible Hype Train types.
/// </summary>
/// <param name="Value">The string value of the Hype Train type.</param>
[Wrapper<string>]
public readonly partial record struct HypeTrainType(string Value)
{
    public static HypeTrainType Treasure { get; } = new("treasure");
    public static HypeTrainType GoldenKappa { get; } = new("golden_kappa");
    public static HypeTrainType Regular { get; } = new("regular");
}
