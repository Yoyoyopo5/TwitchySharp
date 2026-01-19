using System.Net.Http;
using System.Web;
using TwitchySharp.Helpers;

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
    /// <param name="query">The search string.</param>
    /// <param name="first">
    /// The maximum number of items to return per page in the response. 
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </param>
    /// <param name="after">
    /// The cursor used to get the next page of results. 
    /// The <see cref="Pagination"/> object in the response contains the cursor’s value. 
    /// </param>
    public SearchCategoriesRequest(
        string clientId,
        string accessToken,
        string query,
        int? first = null,
        string? after = null
        ) : base(
            "/search/categories",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("query", HttpUtility.UrlEncode(query))
                .Add("first", first?.ToString())
                .Add("after", after)
            )
    {
        Method = HttpMethod.Get;
    }
}
