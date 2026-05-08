namespace TwitchySharp.Api;
/// <summary>
/// Contains information used to page through a list of results. 
/// The <see cref="Cursor"/> is <see langword="null"/> if there are no more pages left to page through.
/// See <see href="https://dev.twitch.tv/docs/api/guide/#pagination">pagination</see> for more information.
/// </summary>
public readonly record struct Pagination
{
    /// <summary>
    /// The cursor used to get the next page of results.
    /// </summary>
    /// <remarks>
    /// This is <see langword="null"/> if the there are no more pages to get.
    /// </remarks>
    public PaginationCursor? Cursor { get; init; }
}
