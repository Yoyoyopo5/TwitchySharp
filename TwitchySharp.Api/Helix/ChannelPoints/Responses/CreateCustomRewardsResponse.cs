using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchySharp.Api.Helix.ChannelPoints;
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
