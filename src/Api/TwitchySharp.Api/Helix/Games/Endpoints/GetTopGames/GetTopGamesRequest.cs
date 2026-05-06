using System.Net.Http;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Games;
/// <summary>
/// Gets information about top games on Twitch.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-top-games">Get Top Games</see> for more information.
/// </remarks>
public record GetTopGamesRequest
    : TwitchHelixRequest<GetTopGamesResponse>, IPageableRequest
{
    protected override string Path => "/games/top";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("first", First?.ToString())
            .Add("after", After?.ToString())
            .Add("before", Before?.ToString());

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }

    /// <summary>
    /// The cursor of the result to get results before.
    /// </summary>
    public PaginationCursor? Before { get; init; }
}
