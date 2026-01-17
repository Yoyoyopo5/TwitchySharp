using TwitchySharp.Api.Models.Helix.ChannelPoints.Requests;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.ChannelPoints.Enums;

/// <summary>
/// Contains static definitions for possible sorting values for a <see cref="GetCustomRewardRedemptionRequest"/>.
/// </summary>
/// <param name="Value">The string value of the sorting method.</param>
public record CustomRewardRedemptionSortingMethod(string Value) : ValueBackedEnum<string>(Value)
{
    public static CustomRewardRedemptionSortingMethod Oldest { get; } = new("OLDEST");
    public static CustomRewardRedemptionSortingMethod Newest { get; } = new("NEWEST");
}
