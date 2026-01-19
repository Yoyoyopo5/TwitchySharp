namespace TwitchySharp.Api.Helix.HypeTrain;
/// <summary>
/// Contains a list of Hype Train events.
/// </summary>
public record GetHypeTrainEventsResponse
{
    /// <summary>
    /// The list of Hype Train events.
    /// The list is empty if the broadcaster hasn’t run a Hype Train within the last 5 days.
    /// </summary>
    public required HypeTrainEvent[] Data { get; init; }
    /// <summary>
    /// Contains the information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is <see langword="null"/> if there are no more pages left to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
}