using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Contains static definitions for cost types for Twitch extension transactions.
/// </summary>
/// <param name="Value">The string value of the cost type.</param>
[Wrapper<string>]
public readonly partial record struct ExtensionProductCostType(string Value)
{
    public static ExtensionProductCostType Bits { get; } = new("bits");
}
