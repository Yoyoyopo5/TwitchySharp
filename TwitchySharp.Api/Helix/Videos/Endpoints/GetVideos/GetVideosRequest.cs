using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Gets information about one or more published videos. 
/// </summary>
/// <remarks>
/// You may get videos by id, by user, or by game/category.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-videos">Get Videos</see> for more information.
/// </remarks>
public record GetVideosRequest
    : TwitchHelixRequest<GetVideosResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="parameters">
    /// The request parameters.
    /// Use derived classes <see cref="VideoIdQuery"/>, <see cref="VideoUserQuery"/>, and <see cref="VideoGameQuery"/> to create a valid query.
    /// </param>
    public GetVideosRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetVideosRequestParameters parameters
        ) : base(
            "/videos",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("id", parameters.Ids?.Select(x => x.Value))
                .Add("user_id", parameters.UserId)
                .Add("game_id", parameters.GameId)
                .Add("language", parameters.Language)
                .Add("period", parameters.Period?.Value)
                .Add("sort", parameters.Sort?.Value)
                .Add("type", parameters.Type?.Value)
                .Add("first", parameters.First?.ToString())
                .Add("after", parameters.After?.Value)
                .Add("before", parameters.Before?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Query for videos based on video id(s).
/// </summary>
public record VideoIdQuery
    : GetVideosRequestParameters
{
    /// <inheritdoc cref="VideoIdQuery"/>
    /// <param name="ids"><inheritdoc cref="GetVideosRequestParameters.Ids" path="/summary"/></param>
    public VideoIdQuery(IEnumerable<VideoId> ids)
        => Ids = ids;
}

/// <summary>
/// Get a list of videos created by a specific broadcaster.
/// </summary>
public record VideoUserQuery
    : PageableVideoQuery
{
    /// <summary>
    /// <inheritdoc cref="VideoUserQuery"/>
    /// </summary>
    /// <param name="userId"><inheritdoc cref="GetVideosRequestParameters.UserId" path="/summary"/></param>
    public VideoUserQuery(UserId userId)
        => UserId = userId;
}

/// <summary>
/// Get videos made of a specific game or category.
/// </summary>
public record VideoGameQuery
    : PageableVideoQuery
{
    /// <summary>
    /// <inheritdoc cref="VideoGameQuery"/>
    /// </summary>
    /// <param name="gameId">
    /// <inheritdoc cref="GetVideosRequestParameters.GameId" path="/summary"/>
    /// </param>
    public VideoGameQuery(GameId gameId)
        => GameId = gameId;

    public new LanguageCode? Language { get => base.Language; set => base.Language = value; }
}

/// <summary>
/// Use derived types <see cref="VideoUserQuery"/> and <see cref="VideoGameQuery"/>.
/// </summary>
public abstract record PageableVideoQuery : GetVideosRequestParameters, IPageableRequest
{
    /// <inheritdoc cref="GetVideosRequestParameters.Period"/>
    public new VideoQueryPeriod? Period { get => base.Period; set => base.Period = value; }
    /// <inheritdoc cref="GetVideosRequestParameters.Sort"/>
    public new VideoQuerySort? Sort { get => base.Sort; set => base.Sort = value; }
    /// <inheritdoc cref="GetVideosRequestParameters.Type"/>
    public new VideoQueryType? Type { get => base.Type; set => base.Type = value; }

    /// <inheritdoc cref="GetVideosRequestParameters.First"/>
    public new PaginationAmount? First { get => base.First; set => base.First = value; }
    /// <inheritdoc cref="GetVideosRequestParameters.After"/>
    public new PaginationCursor? After { get => base.After; set => base.After = value; }
    /// <inheritdoc cref="GetVideosRequestParameters.Before"/>
    public new PaginationCursor? Before { get => base.Before; set => base.Before = value; }
}

/// <summary>
/// Abstract class used to form a <see cref="GetVideosRequest"/>.
/// Use derived classes <see cref="VideoIdQuery"/>, <see cref="VideoUserQuery"/>, and <see cref="VideoGameQuery"/> to create valid queries.
/// </summary>
public abstract record GetVideosRequestParameters
{
    /// <summary>
    /// A list of ids of the videos to get.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 ids. 
    /// The API ignores duplicate ids and ids that weren't found (if there's at least one valid id).
    /// </remarks>
    public IEnumerable<VideoId>? Ids { get; protected set; }
    /// <summary>
    /// The user id of the broadcaster whose list of videos you want to get.
    /// </summary>
    public UserId? UserId { get; protected set; }
    /// <summary>
    /// The id of the game or category you want to get videos for.
    /// </summary>
    public GameId? GameId { get; protected set; }
    /// <summary>
    /// An ISO 639-1 two-letter code to filter returned videos by.
    /// </summary>
    /// <remarks>
    /// For a list of supported languages, see <see href="https://help.twitch.tv/s/article/languages-on-twitch#streamlang">Supported Stream Language</see>. 
    /// If the language is not supported, use <see cref="LanguageCode.Other"/>.
    /// </remarks>
    public LanguageCode? Language { get; protected set; }
    /// <summary>
    /// Filters the returned list of videos by when they were published.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="VideoQueryPeriod.All"/>.
    /// </remarks>
    public VideoQueryPeriod? Period { get; protected set; }
    /// <summary>
    /// The sort order to return the videos in.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="VideoQuerySort.Time"/>.
    /// </remarks>
    public VideoQuerySort? Sort { get; protected set; }
    /// <summary>
    /// Filters the returned list of videos by type.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="VideoQueryType.All"/>.
    /// </remarks>
    public VideoQueryType? Type { get; protected set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; protected set; }
    public PaginationCursor? After { get; protected set; }
    /// <summary>
    /// The cursor of the result to get results before.
    /// </summary>
    public PaginationCursor? Before { get; protected set; }
}