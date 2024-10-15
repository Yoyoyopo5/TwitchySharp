using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.ChannelPoints;
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
