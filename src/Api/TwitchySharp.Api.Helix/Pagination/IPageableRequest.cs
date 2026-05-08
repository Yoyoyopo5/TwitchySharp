namespace TwitchySharp.Api;

/// <summary>
/// Supports fetching pages via a <see cref="PaginationCursor"/> and <see cref="PaginationAmount"/>.
/// </summary>
public interface IPageableRequest
{
    /// <summary>
    /// The cursor of the result to get results after.
    /// </summary>
    /// <remarks>
    /// This value can be obtained from a <see cref="Pagination"/> object inside of a <see cref="IPageableResponse"/>.
    /// Set this value to that value to get the next page of results.
    /// </remarks>
    PaginationCursor? After { get; init; }
    /// <summary>
    /// The maximum number of results to include per page in the response.
    /// </summary>
    PaginationAmount? First { get; init; }
}
