using TwitchySharp.Api.Models.Helix.ChannelPoints.Models;

namespace TwitchySharp.Api.Models.Helix.ChannelPoints.Responses;
/// <summary>
/// Contains a list of custom rewards.
/// </summary>
public record GetCustomRewardResponse
{
    /// <summary>
    /// A list of custom rewards. 
    /// The list is in ascending order by id. 
    /// If the broadcaster hasn’t created custom rewards, the list is empty.
    /// </summary>
    public required CustomChannelPointsReward[] Data { get; init; }
}
