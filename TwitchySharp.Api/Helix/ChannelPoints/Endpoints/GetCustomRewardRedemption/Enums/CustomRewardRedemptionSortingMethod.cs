using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.ChannelPoints;

/// <summary>
/// Contains static definitions for possible sorting values for a <see cref="GetCustomRewardRedemptionRequest"/>.
/// </summary>
/// <param name="Value">The string value of the sorting method.</param>
[Wrapper<string>]
public readonly partial record struct CustomRewardRedemptionSortingMethod(string Value)
{
    public static CustomRewardRedemptionSortingMethod Oldest { get; } = new("OLDEST");
    public static CustomRewardRedemptionSortingMethod Newest { get; } = new("NEWEST");
}
