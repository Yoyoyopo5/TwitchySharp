namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific channel prediction outcome.
/// </summary>
public record ChannePredictionOutcome
{
    /// <summary>
    /// The id of the outcome.
    /// </summary>
    public required PredictionOutcomeId Id { get; init; }
    /// <summary>
    /// The title of the outcome.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The color of the outcome.
    /// </summary>
    public required ChannelPredictionColor Color { get; init; }
    /// <summary>
    /// The total number of users who chose this outcome.
    /// </summary>
    public required int Users { get; init; }
    /// <summary>
    /// The total number of Channel Points bet on this outcome.
    /// </summary>
    public required int ChannelPoints { get; init; }
    /// <summary>
    /// An array of users who used the highest amount of Channel Points on this outcome.
    /// </summary>
    /// <remarks>
    /// Dev Note: I believe this is at most 10 users. Not sure if the array is sorted.
    /// </remarks>
    public required ChannelPredictionPredictor[] TopPredictors { get; init; }
}
