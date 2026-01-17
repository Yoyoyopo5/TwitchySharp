using TwitchySharp.Api.Models.Helix.Predictions.Models;

namespace TwitchySharp.Api.Models.Helix.Predictions.Responses;
/// <summary>
/// Contains information about the ended prediction.
/// </summary>
public record EndPredictionResponse
{
    /// <summary>
    /// A list containing the single prediction that was ended.
    /// </summary>
    public required ChatPrediction[] Data { get; init; }
}
