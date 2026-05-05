namespace TwitchySharp.Api.Helix.Clips;
/// <summary>
/// Contains a list of Twitch clips.
/// </summary>
public record GetClipsResponse
    : IPageableResponse
{
    /// <summary>
    /// The list of video clips.
    /// For clips returned by GameId or BroadcasterId, the list is in descending order by view count. 
    /// For lists returned by id, the list is in the same order as the input ids.
    /// </summary>
    public required TwitchClip[] Data { get; init; }
    /// <summary>
    /// The information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is <see langword="null"/> if there are no more pages left to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
}