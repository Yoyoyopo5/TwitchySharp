namespace TwitchySharp.Api;

public interface IPageableResponse
{
    /// <summary>
    /// Contains the <see cref="PaginationCursor"/> needed to get the next page of results.
    /// </summary>
    Pagination Pagination { get; }
}
