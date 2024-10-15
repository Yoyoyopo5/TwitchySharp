using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.ChannelPoints;
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
