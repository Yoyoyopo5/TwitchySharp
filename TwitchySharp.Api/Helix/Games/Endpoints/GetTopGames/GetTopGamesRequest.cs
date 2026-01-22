using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Games;
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
    /// <param name="parameters">The request parameters.</param>
    public GetTopGamesRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetTopGamesRequestParameters? parameters = null
        ) : base(
            "/games/top",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("first", parameters?.First?.ToString())
                .Add("after", parameters?.After?.Value)
                .Add("before", parameters?.Before?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetTopGamesRequest"/>.
/// </summary>
public record GetTopGamesRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
    /// <summary>
    /// The cursor of the result to get results before.
    /// </summary>
    public PaginationCursor? Before { get; set; }
}