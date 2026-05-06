using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Predictions;

/// <summary>
/// Contains information about a specific top predictor for a prediction.
/// </summary>
public record ChatPredictionTopPredictor
{
    /// <summary>
    /// The user id of the top predictor.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The display name of the top predictor.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The login (username) of the top predictor.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The number of Channel Points this top predictor used.
    /// </summary>
    public required int ChannelPointsUsed { get; init; }
    /// <summary>
    /// The number of Channel Points this predictor won.
    /// </summary>
    public required int ChannelPointsWon { get; init; }
}
