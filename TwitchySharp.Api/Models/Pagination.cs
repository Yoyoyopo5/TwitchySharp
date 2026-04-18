using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

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

/// <summary>
/// A cursor used for pagination.
/// </summary>
/// <param name="Value">The cursor's string value.</param>
[Wrapper<string>]
public readonly partial record struct PaginationCursor(string Value);

/// <summary>
/// Represents the amount of results per page to fetch.
/// </summary>
/// <param name="Value">The integer value of the amount.</param>
[Wrapper<int>]
public readonly partial record struct PaginationAmount(int Value);

public interface IPageableResponse
{
    /// <summary>
    /// Contains the <see cref="PaginationCursor"/> needed to get the next page of results.
    /// </summary>
    Pagination Pagination { get; }
}

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