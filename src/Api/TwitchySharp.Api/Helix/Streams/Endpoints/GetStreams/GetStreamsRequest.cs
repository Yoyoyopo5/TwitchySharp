using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Gets a list of all streams.
/// </summary>
/// <remarks>
/// The returned list will be in descending order by the number of viewers watching the stream.
/// Because viewers come and go during a stream, it's possible to find duplicate or missing streams in the list as you page through the results.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-streams">Get Streams</see> for more information.
/// </remarks>
public record GetStreamsRequest
    : TwitchHelixRequest<GetStreamsResponse>, IPageableRequest
{
    protected override string Path => "/streams";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("user_id", UserIds?.Select(x => x.Value))
            .Add("user_login", UserLogins?.Select(x => x.Value))
            .Add("game_id", GameIds?.Select(x => x.Value))
            .Add("type", Type?.Value)
            .Add("language", Languages?.Select(x => x.Value))
            .Add("first", First?.ToString())
            .Add("before", Before?.Value)
            .Add("after", After?.Value);

    /// <summary>
    /// A list of user ids used to filter the list of streams.
    /// Returns only the streams of those users that are broadcasting.
    /// You may specify a maximum of 100 ids.
    /// </summary>
    public IEnumerable<UserId>? UserIds { get; init; }
    /// <summary>
    /// A list of user logins (usernames) used to filter the list of streams.
    /// Returns only the streams of those users that are broadcasting.
    /// You may specify a maximum of 100 login names.
    /// </summary>
    public IEnumerable<UserLogin>? UserLogins { get; init; }
    /// <summary>
    /// A game (category) id used to filter the list of streams.
    /// Returns only the streams that are broadcasting the game (category).
    /// You may specify a maximum of 100 ids.
    /// </summary>
    public IEnumerable<GameId>? GameIds { get; init; }
    /// <summary>
    /// The type of stream to filter the list of streams by.
    /// The default is <see cref="StreamType.All"/>.
    /// </summary>
    public StreamType? Type { get; init; }
    /// <summary>
    /// A language code used to filter the list of streams.
    /// Returns only streams that broadcast in the specified language.
    /// Specify the language using an ISO 639-1 two-letter language code or other if the broadcast uses a language not in the list of <see href="https://help.twitch.tv/s/article/languages-on-twitch#streamlang">supported stream languages</see>.
    /// You may specify a maximum of 100 language codes.
    /// </summary>
    public IEnumerable<LanguageCode>? Languages { get; init; }
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
