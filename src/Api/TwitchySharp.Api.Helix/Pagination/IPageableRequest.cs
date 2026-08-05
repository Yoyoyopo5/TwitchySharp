namespace TwitchySharp.Api;

/// <summary>
/// Supports fetching pages via a <see cref="PaginationCursor"/> and <see cref="PaginationAmount"/>.
/// </summary>
public interface IPageableRequest
{
    /// <summary>
    /// The cursor of the result to get results after.
    /// </summary>
    PaginationCursor? After { get; init; }
    /// <summary>
    /// The maximum number of results to include per page in the response.
    /// </summary>
    PaginationAmount? First { get; init; } // Consider removing
}
