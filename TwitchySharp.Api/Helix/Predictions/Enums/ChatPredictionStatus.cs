using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Contains static definitions for possible chat prediction statuses.
/// </summary>
/// <param name="Value">The string value of the chat prediction status.</param>
[Wrapper<string>]
public readonly partial record struct ChatPredictionStatus(string Value)
{
    /// <summary>
    /// The Prediction is running and viewers can make predictions.
    /// </summary>
    public static ChatPredictionStatus Active { get; } = new("ACTIVE");
    /// <summary>
    /// The broadcaster canceled the Prediction and refunded the Channel Points to the participants.
    /// </summary>
    public static ChatPredictionStatus Cancelled { get; } = new("CANCELLED");
    /// <summary>
    /// The broadcaster locked the Prediction, which means viewers can no longer make predictions.
    /// </summary>
    public static ChatPredictionStatus Locked { get; } = new("LOCKED");
    /// <summary>
    /// The winning outcome was determined and the Channel Points were distributed to the viewers who predicted the correct outcome.
    /// </summary>
    public static ChatPredictionStatus Resolved { get; } = new("RESOLVED");
}
