using TwitchySharp.EventSub.Models.Events.Channel.Predictions;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.Predictions;

/// <summary>
/// A channel chat prediction.
/// </summary>
public interface IHavePrediction
{
    /// <summary>
    /// The id of the prediction.
    /// </summary>
    string Id { get; }
    /// <summary>
    /// The title of the prediction.
    /// </summary>
    string Title { get; }
    /// <summary>
    /// The outcomes of the prediction.
    /// </summary>
    ChannePredictionOutcome[] Outcomes { get; }
    /// <summary>
    /// The date and time the prediction started.
    /// </summary>
    DateTimeOffset StartedAt { get; }
}
