using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Games;
/// <summary>
/// Gets information about top games on Twitch.
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-top-games">get top games</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// </remarks>
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
public class GetTopGamesRequest(string clientId, string accessToken, int? first = null, string? after = null, string? before = null)
    : HelixApiRequest<GetTopGamesResponse>(
        "/games/top" + 
        new HttpQueryParameters()
            .Add("first", first?.ToString())
            .Add("after", after)
            .Add("before", before),
        clientId,
        accessToken
        );
