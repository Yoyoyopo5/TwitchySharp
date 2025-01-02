using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Predictions;

/// <summary>
/// Contains information about a specific prediction outcome.
/// </summary>
public record ChatPredictionOutcome
{
    /// <summary>
    /// The id of the outcome.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The outcome’s text as it is displayed to viewers.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The number of unique viewers that picked this outcome.
    /// </summary>
    public required int Users { get; init; }
    /// <summary>
    /// The number of Channel Points spent by viewers on this outcome.
    /// </summary>
    public required int ChannelPoints { get; init; }
    /// <summary>
    /// A list of viewers who were the top predictors.
    /// This is <see langword="null"/> if there are no predictors.
    /// </summary>
    public ChatPredictionTopPredictor[]? TopPredictors { get; init; }
    /// <summary>
    /// The color that visually identifies this outcome in the UX.
    /// If there are only two outcomes, the color is <see cref="ChatPredictionOutcomeColor.Blue"/> for the first outcome and <see cref="ChatPredictionOutcomeColor.Pink"/> for the second outcome. 
    /// If there are more than two outcomes, the color is <see cref="ChatPredictionOutcomeColor.Blue"/> for all outcomes.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseUpperJsonStringEnumConverter<ChatPredictionOutcomeColor>))]
    public required ChatPredictionOutcomeColor Color { get; init; }
}
