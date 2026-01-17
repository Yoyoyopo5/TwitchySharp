using TwitchySharp.Api.Models.Helix.ChannelPoints.Models;

namespace TwitchySharp.Api.Models.Helix.ChannelPoints.Responses;
/// <summary>
/// Contains information on updated reward redemptions.
/// </summary>
public record UpdateRedemptionStatusResponse
{
    /// <summary>
    /// A list containing the single redemption that was updated.
    /// </summary>
    public required CustomRewardRedemption[] Data { get; init; }
}
