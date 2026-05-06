namespace TwitchySharp.Api.Helix.Predictions;
/// <summary>
/// Contains information about a channel's past and current predictions.
/// </summary>
public record GetPredictionsResponse
    : IPageableResponse
{
    /// <summary>
    /// The list of predictions.
    /// The list is sorted in descending order by <see cref="ChatPrediction.CreatedAt"/>.
    /// The list is empty if the broadcaster has not created any predictions.
    /// </summary>
    public required ChatPrediction[] Data { get; init; }
    /// <inheritdoc cref="Api.Pagination"/>
    public required Pagination Pagination { get; init; }
}
