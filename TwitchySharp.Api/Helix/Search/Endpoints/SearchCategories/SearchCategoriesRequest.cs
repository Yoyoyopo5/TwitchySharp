using System.Net.Http;
using System.Web;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Search;
/// <summary>
/// Gets the games or categories that match the specified query.
/// </summary>
/// <remarks>
/// To match, the category’s name must contain all parts of the query string. 
/// For example, if the query string is 42, the response includes any category name that contains 42 in the title. 
/// If the query string is a phrase like love computer, the response includes any category name that contains the words love and computer anywhere in the name. 
/// The comparison is case insensitive.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#search-categories">Search Categories</see> for more information.
/// </remarks>
public record SearchCategoriesRequest
    : TwitchHelixRequest<SearchCategoriesResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="parameters">The request parameters.</param>
    public SearchCategoriesRequest(
        ClientId clientId,
        AccessToken accessToken,
        SearchCategoriesRequestParameters parameters
        ) : base(
            "/search/categories",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("query", parameters.Query)
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="SearchCategoriesRequest"/>.
/// </summary>
public record SearchCategoriesRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The search string.
    /// </summary>
    public required string Query { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
}
