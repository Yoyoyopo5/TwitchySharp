namespace TwitchySharp.Api.Helix.Predictions;

/// <summary>
/// Data used to create an individual outcome for a new prediction.
/// </summary>
public record CreatePredictionOutcome
{
    /// <summary>
    /// The text of one of the outcomes that the viewer may select. 
    /// The title is limited to a maximum of 25 characters.
    /// </summary>
    public required string Title { get; init; }
}
