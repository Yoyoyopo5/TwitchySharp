using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Contains information about a channel's past and current predictions.
/// </summary>
public record GetPredictionsResponse
{
    /// <summary>
    /// The list of predictions.
    /// The list is sorted in descending order by <see cref="ChatPrediction.CreatedAt"/>.
    /// The list is empty if the broadcaster has not created any predictions.
    /// </summary>
    public required ChatPrediction[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}