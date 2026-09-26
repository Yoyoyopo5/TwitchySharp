namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Contains information about a newly created prediction.
/// </summary>
public record CreatePredictionResponseContent
{
    /// <summary>
    /// A list containing the single prediction that was created.
    /// </summary>
    public required ChatPrediction[] Data { get; init; }
}
