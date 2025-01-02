namespace TwitchySharp.Api.Helix.Predictions;

/// <summary>
/// Contains information about a specific top predictor for a prediction.
/// </summary>
public record ChatPredictionTopPredictor
{
    /// <summary>
    /// The user id of the top predictor.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the top predictor.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The login (username) of the top predictor.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The number of Channel Points this top predictor used.
    /// </summary>
    public required int ChannelPointsUsed { get; init; }
    /// <summary>
    /// The number of Channel Points this predictor won.
    /// </summary>
    public required int ChannelPointsWon { get; init; }
}
