using TwitchySharp.Api.Models.Helix.Predictions.Models;

namespace TwitchySharp.Api.Models.Helix.Predictions.Responses;
/// <summary>
/// Contains information about a newly created prediction.
/// </summary>
public record CreatePredictionResponse
{
    /// <summary>
    /// A list containing the single prediction that was created.
    /// </summary>
    public required ChatPrediction[] Data { get; init; }
}
