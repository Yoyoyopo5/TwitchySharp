using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Search;
/// <summary>
/// Gets the channels that match the specified query and have streamed content within the past 6 months.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#search-channels">search channels</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="query">
/// The query string to search channels with.
/// <para>
/// The request will return channels where the beginning of the broadcaster’s name or category matches the <paramref name="query"/>.
/// The comparison is case insensitive.
/// If <paramref name="query"/> is <c>"angel_of_death"</c>, it matches all names that begin with angel_of_death.
/// However, if <paramref name="query"/> is a phrase like <c>"angel of death"</c>, it matches to names starting with angelofdeath or names starting with angel_of_death.
/// </para>
/// </param>
/// <param name="liveOnly">
/// Determines whether the response includes only channels that are currently streaming live. Defaults to <see langword="false"/>.
/// <para>
/// The comparison also depends on the value of this parameter.
/// If it is <see langword="false"/>, the API matches on the broadcaster’s login (username). 
/// However, if it is <see langword="true"/>, the API matches on the broadcaster’s name and category name.
/// </para>
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 100 items per page. 
/// The default is 20.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> object in the response contains the cursor’s value.
/// </param>
public class SearchChannelsRequest(
    string clientId,
    string accessToken,
    string query,
    bool? liveOnly = null,
    int? first = null,
    string? after = null
    )
    : HelixApiRequest<SearchChannelsResponse>(
        "/search/channels" +
        new HttpQueryParameters()
            .Add("query", HttpUtility.UrlEncode(query))
            .Add("live_only", liveOnly?.ToString())
            .Add("first", first?.ToString())
            .Add("after", after),
        clientId,
        accessToken
        );
