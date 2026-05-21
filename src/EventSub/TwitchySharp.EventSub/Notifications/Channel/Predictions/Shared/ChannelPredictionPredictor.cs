namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information on a specific user that pariticpated in a channel prediction.
/// </summary>
public record ChannelPredictionPredictor
{
    /// <summary>
    /// The user id of the predictor.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the predictor.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the predictor.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The amount of Channel Points the predictor won in the prediction.
    /// This is <see langword="null"/> if the underlying prediction has not ended.
    /// This is <c>0</c> if the predictor did not guess the correct outcome or
    /// if the prediction was cancelled.
    /// </summary>
    public int? ChannelPointsWon { get; init; }
    /// <summary>
    /// The amount of Channel Points the predictor used to participate in the prediction.
    /// </summary>
    public required int ChannelPointsUsed { get; init; }
}
