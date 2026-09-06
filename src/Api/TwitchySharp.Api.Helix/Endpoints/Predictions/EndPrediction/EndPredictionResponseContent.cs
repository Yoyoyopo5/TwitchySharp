namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Contains information about the ended prediction.
/// </summary>
public record EndPredictionResponseContent
{
    /// <summary>
    /// A list containing the single prediction that was ended.
    /// </summary>
    public required ChatPrediction[] Data { get; init; }
}
