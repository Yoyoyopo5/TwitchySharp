using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Contains a list of custom reward redemptions.
/// </summary>
public record GetCustomRewardRedemptionResponse
{
    /// <summary>
    /// The list of redemptions for the specified reward.
    /// The list is empty if there are no redemptions that match the redemption criteria.
    /// </summary>
    public required CustomRewardRedemption[] Data { get; init; }
    public Pagination? Pagination { get; init; } // This property is not included in the API docs for a response, but it is alluded to in the request parameters. I'm including it here for now as nullable until we test if it is returned by API or not.
}
