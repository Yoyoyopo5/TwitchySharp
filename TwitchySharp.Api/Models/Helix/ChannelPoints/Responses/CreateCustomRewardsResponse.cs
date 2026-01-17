using TwitchySharp.Api.Models.Helix.ChannelPoints.Models;

namespace TwitchySharp.Api.Models.Helix.ChannelPoints.Responses;
/// <summary>
/// Contains a list of created custom rewards.
/// </summary>
public record CreateCustomRewardsResponse
{
    /// <summary>
    /// A list that contains the single custom reward you created.
    /// </summary>
    public required CustomChannelPointsReward[] Data { get; init; }
}
