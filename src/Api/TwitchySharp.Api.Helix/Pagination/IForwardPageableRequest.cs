namespace TwitchySharp.Api;

/// <summary>
/// Supports fetching subsequent pages of a request via a <see cref="PaginationCursor"/>.
/// </summary>
public interface IForwardPageableRequest
{
    /// <summary>
    /// The cursor of the result to get results after.
    /// </summary>
    PaginationCursor? After { get; init; }
}
