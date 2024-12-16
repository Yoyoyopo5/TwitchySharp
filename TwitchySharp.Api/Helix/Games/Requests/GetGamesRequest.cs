using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Games;
/// <summary>
/// Gets information about specified categories or games.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-games">get games</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="games">
/// The games to get data for.
/// You may specify up to 100 games.
/// </param>
public class GetGamesRequest(string clientId, string accessToken, IEnumerable<GameQuery> games)
    : HelixApiRequest<GetGamesResponse>(
        "/games" +
        new HttpQueryParameters()
            .Add("id", games.GetValues(GameQueryType.Id))
            .Add("name", games.GetValues(GameQueryType.Name))
            .Add("igdb_id", games.GetValues(GameQueryType.Igdb)),
        clientId,
        accessToken
        );

public enum GameQueryType
{
    Id,
    Name,
    Igdb
}

/// <summary>
/// Used in the <see cref="GetGamesRequest"/> API endpoint.
/// Use derived classes <see cref="GameIdQuery"/>, <see cref="GameNameQuery"/>, and <see cref="GameIgdbQuery"/> to filter the request.
/// </summary>
/// <param name="Type">The type of filter.</param>
/// <param name="Value">The value of the filter.</param>
public abstract record GameQuery(GameQueryType Type, string Value);
/// <summary>
/// Query games by game id.
/// </summary>
/// <param name="GameId">The game id to get.</param>
public record GameIdQuery(string GameId) : GameQuery(GameQueryType.Id, GameId);
/// <summary>
/// Query games by game name.
/// </summary>
/// <param name="GameName">The name of the game to get.</param>
public record GameNameQuery(string GameName) : GameQuery(GameQueryType.Name, GameName);
/// <summary>
/// Query games by <see href="https://www.igdb.com/">IGDB</see> id.
/// </summary>
/// <param name="IgdbId">The IGDB id of the game to get.</param>
public record GameIgdbQuery(string IgdbId) : GameQuery(GameQueryType.Igdb, IgdbId);

internal static class GameQueryEnumerableExtensions
{
    public static IEnumerable<string> GetValues(this IEnumerable<GameQuery> gameQueries, GameQueryType filter)
        => gameQueries.Where(x => x.Type == filter).Select(x => x.Value);
}