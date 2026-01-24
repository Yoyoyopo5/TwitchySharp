using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Search;
/// <summary>
/// Gets the channels that match the specified query and have streamed content within the past 6 months.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#search-channels">Search Channels</see> for more information.
/// </remarks>
public record SearchChannelsRequest
    : TwitchHelixRequest<SearchChannelsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="parameters">The request parameters.</param>
    public SearchChannelsRequest(
        ClientId clientId,
        AccessToken accessToken,
        SearchChannelsRequestParameters parameters
        ) : base(
            "/search/channels",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("query", parameters.Query)
                .Add("live_only", parameters.LiveOnly?.ToString())
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="SearchChannelsRequest"/>.
/// </summary>
public record SearchChannelsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The query string to search channels with.
    /// </summary>
    /// <remarks>
    /// The request will return channels where the beginning of the broadcaster’s name or category matches the query.
    /// The comparison is case insensitive.
    /// If query is <c>"angel_of_death"</c>, it matches all names that begin with angel_of_death.
    /// However, if query is a phrase like <c>"angel of death"</c>, it matches to names starting with angelofdeath or names starting with angel_of_death.
    /// </remarks>
    public required string Query { get; set; }
    /// <summary>
    /// Determines whether the response includes only channels that are currently streaming live.
    /// </summary>
    /// <remarks>
    /// Defaults to <see langword="false"/>.
    /// The comparison also depends on the value of this parameter.
    /// If it is <see langword="false"/>, the API matches on the broadcaster’s login (username). 
    /// However, if it is <see langword="true"/>, the API matches on the broadcaster’s name and category name.
    /// </remarks>
    public bool? LiveOnly { get; set; }
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
