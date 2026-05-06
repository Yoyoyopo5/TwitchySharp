using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Games;
/// <summary>
/// Gets information about specified categories or games.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-games">Get Games</see> for more information.
/// </remarks>
public record GetGamesRequest
    : TwitchHelixRequest<GetGamesResponse>
{
    protected override string Path => "/games";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", Games.OfType<GameIdQuery>().Select(x => x.GameId.Value))
            .Add("name", Games.OfType<GameNameQuery>().Select(x => x.GameName))
            .Add("igdb_id", Games.OfType<GameIgdbQuery>().Select(x => x.IgdbId.Value));

    /// <summary>
    /// The games to get data for.
    /// </summary>
    /// <remarks>
    /// You may specify up to 100 games.
    /// Use derived classes <see cref="GameIdQuery"/>, <see cref="GameNameQuery"/>, and <see cref="GameIgdbQuery"/>.
    /// </remarks>
    public required IEnumerable<GameQuery> Games { get; init; }
}


/// <summary>
/// Used in the <see cref="GetGamesRequest"/> API endpoint.
/// </summary>
/// <remarks>
/// Use derived classes <see cref="GameIdQuery"/>, <see cref="GameNameQuery"/>, and <see cref="GameIgdbQuery"/> to filter the request.
/// </remarks>
public abstract record GameQuery();
/// <summary>
/// Query games by game id.
/// </summary>
/// <param name="GameId">The game id to get.</param>
public record GameIdQuery(GameId GameId) : GameQuery();
/// <summary>
/// Query games by game name.
/// </summary>
/// <param name="GameName">The name of the game to get.</param>
public record GameNameQuery(string GameName) : GameQuery();
/// <summary>
/// Query games by <see href="https://www.igdb.com/">IGDB</see> id.
/// </summary>
/// <param name="IgdbId">The IGDB id of the game to get.</param>
public record GameIgdbQuery(IgdbId IgdbId) : GameQuery();