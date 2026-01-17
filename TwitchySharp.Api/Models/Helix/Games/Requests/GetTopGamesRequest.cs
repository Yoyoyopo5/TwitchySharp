using System.Net.Http;
using TwitchySharp.Api.Models.Helix.Games.Responses;
using TwitchySharp.Api.Models.Shared;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Games.Requests;
/// <summary>
/// Gets information about top games on Twitch.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-top-games">Get Top Games</see> for more information.
/// </remarks>
public record GetTopGamesRequest : TwitchHelixRequest<GetTopGamesResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="first">
    /// The maximum number of items to return per page in the response. 
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </param>
    /// <param name="after">
    /// The cursor used to get the next page of results. 
    /// The <see cref="Pagination"/> property in the response contains the cursor’s value. 
    /// </param>
    /// <param name="before">
    /// The cursor used to get the previous page of results. 
    /// The <see cref="Pagination"/> property in the response contains the cursor’s value. 
    /// </param>
    public GetTopGamesRequest(
        string clientId,
        string accessToken,
        int? first = null,
        string? after = null,
        string? before = null
        ) : base(
            "/games/top",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("first", first?.ToString())
                .Add("after", after)
                .Add("before", before)
            )
    {
        Method = HttpMethod.Get;
    }
}