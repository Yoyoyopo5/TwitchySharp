namespace TwitchySharp.Api.Helix.Predictions;

/// <summary>
/// Possible statuses for a chat prediction.
/// </summary>
public enum ChatPredictionStatus
{
    /// <summary>
    /// The Prediction is running and viewers can make predictions.
    /// </summary>
    Active,
    /// <summary>
    /// The broadcaster canceled the Prediction and refunded the Channel Points to the participants.
    /// </summary>
    Cancelled,
    /// <summary>
    /// The broadcaster locked the Prediction, which means viewers can no longer make predictions.
    /// </summary>
    Locked,
    /// <summary>
    /// The winning outcome was determined and the Channel Points were distributed to the viewers who predicted the correct outcome.
    /// </summary>
    Resolved
}
