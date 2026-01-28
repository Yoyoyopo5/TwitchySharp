using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Gets information about one or more published videos.
/// </summary>
/// <remarks>
/// You may get videos by id, by user, or by game/category.
/// One of <see cref="Ids"/>, <see cref="UserId"/>, or <see cref="GameId"/> must be specified.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-videos">Get Videos</see> for more information.
/// </remarks>
public record GetVideosRequest
    : TwitchHelixRequest<GetVideosResponse>, IPageableRequest
{
    protected override string Path => "/videos";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", Ids?.Select(x => x.Value))
            .Add("user_id", UserId)
            .Add("game_id", GameId)
            .Add("language", Language)
            .Add("period", Period?.Value)
            .Add("sort", Sort?.Value)
            .Add("type", Type?.Value)
            .Add("first", First?.ToString())
            .Add("after", After?.Value)
            .Add("before", Before?.Value);

    /// <summary>
    /// A list of ids of the videos to get.
    /// </summary>
    /// <remarks>
    /// Mutually exclusive with <see cref="UserId"/> and <see cref="GameId"/>.
    /// You may specify a maximum of 100 ids.
    /// The API ignores duplicate ids and ids that weren't found (if there's at least one valid id).
    /// </remarks>
    public IEnumerable<VideoId>? Ids { get; init; }
    /// <summary>
    /// The user id of the broadcaster whose list of videos you want to get.
    /// </summary>
    /// <remarks>
    /// Mutually exclusive with <see cref="Ids"/> and <see cref="GameId"/>.
    /// </remarks>
    public UserId? UserId { get; init; }
    /// <summary>
    /// The id of the game or category you want to get videos for.
    /// </summary>
    /// <remarks>
    /// Mutually exclusive with <see cref="Ids"/> and <see cref="UserId"/>.
    /// </remarks>
    public GameId? GameId { get; init; }
    /// <summary>
    /// An ISO 639-1 two-letter code to filter returned videos by.
    /// </summary>
    /// <remarks>
    /// Only applicable when querying by <see cref="GameId"/>.
    /// For a list of supported languages, see <see href="https://help.twitch.tv/s/article/languages-on-twitch#streamlang">Supported Stream Language</see>.
    /// If the language is not supported, use <see cref="LanguageCode.Other"/>.
    /// </remarks>
    public LanguageCode? Language { get; init; }
    /// <summary>
    /// Filters the returned list of videos by when they were published.
    /// </summary>
    /// <remarks>
    /// Only applicable when querying by <see cref="UserId"/> or <see cref="GameId"/>.
    /// Defaults to <see cref="VideoQueryPeriod.All"/>.
    /// </remarks>
    public VideoQueryPeriod? Period { get; init; }
    /// <summary>
    /// The sort order to return the videos in.
    /// </summary>
    /// <remarks>
    /// Only applicable when querying by <see cref="UserId"/> or <see cref="GameId"/>.
    /// Defaults to <see cref="VideoQuerySort.Time"/>.
    /// </remarks>
    public VideoQuerySort? Sort { get; init; }
    /// <summary>
    /// Filters the returned list of videos by type.
    /// </summary>
    /// <remarks>
    /// Only applicable when querying by <see cref="UserId"/> or <see cref="GameId"/>.
    /// Defaults to <see cref="VideoQueryType.All"/>.
    /// </remarks>
    public VideoQueryType? Type { get; init; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// Only applicable when querying by <see cref="UserId"/> or <see cref="GameId"/>.
    /// The minimum page size is 1 item per page and the maximum is 100.
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