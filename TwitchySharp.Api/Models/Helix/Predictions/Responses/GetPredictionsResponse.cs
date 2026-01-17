using TwitchySharp.Api.Models.Helix.Predictions.Models;
using TwitchySharp.Api.Models.Shared;

namespace TwitchySharp.Api.Models.Helix.Predictions.Responses;
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