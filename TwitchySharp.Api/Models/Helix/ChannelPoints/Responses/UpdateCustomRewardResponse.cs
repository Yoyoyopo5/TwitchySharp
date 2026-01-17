using TwitchySharp.Api.Models.Helix.ChannelPoints.Models;

namespace TwitchySharp.Api.Models.Helix.ChannelPoints.Responses;
/// <summary>
/// Contains a list of a single reward that was updated.
/// </summary>
public record UpdateCustomRewardResponse
{
    /// <summary>
    /// Contains the single reward that was updated.
    /// </summary>
    public required CustomChannelPointsReward[] Data { get; init; }
}
