namespace TwitchySharp.Api;

/// <summary>
/// Supports fetching previous pages of a request via a <see cref="PaginationCursor"/>
/// </summary>
public interface IBackwardPageableRequest
{
    /// <summary>
    /// The cursor of the result to get results before.
    /// </summary>
    PaginationCursor? Before { get; init; }
}
