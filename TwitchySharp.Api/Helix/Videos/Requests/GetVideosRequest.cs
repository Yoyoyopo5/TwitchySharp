using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Gets information about one or more published videos. 
/// You may get videos by ID, by user, or by game/category.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-videos">get videos</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="query">
/// The query to use when requesting videos.
/// Use derived classes <see cref="VideoIdQuery"/>, <see cref="VideoUserQuery"/>, and <see cref="VideoGameQuery"/> to create a valid query.
/// </param>
public class GetVideosRequest(
    string clientId,
    string accessToken,
    GetVideosRequestQueryParameters query
    )
    : HelixApiRequest<GetVideosResponse>(
        "/videos" + 
        new HttpQueryParameters()
            .Add("id", query.Ids)
            .Add("user_id", query.UserId)
            .Add("game_id", query.GameId)
            .Add("language", query.Language)
            .Add("period", query.Period?.Value)
            .Add("sort", query.Sort?.Value)
            .Add("type", query.Type?.Value)
            .Add("first", query.First?.ToString())
            .Add("after", query.After)
            .Add("before", query.Before),
        clientId,
        accessToken
        );

/// <summary>
/// Query for videos based on video id(s).
/// </summary>
public record VideoIdQuery
    : GetVideosRequestQueryParameters
{
    /// <inheritdoc cref="VideoIdQuery"/>
    /// <param name="ids"><inheritdoc cref="GetVideosRequestQueryParameters.Ids" path="/summary"/></param>
    public VideoIdQuery(IEnumerable<string> ids)
        => Ids = ids;
}

/// <summary>
/// Get a list of videos created by a specific broadcaster.
/// </summary>
public record VideoUserQuery
    : GetVideosRequestQueryParameters
{
    /// <summary>
    /// <inheritdoc cref="VideoUserQuery"/>
    /// </summary>
    /// <param name="userId"><inheritdoc cref="GetVideosRequestQueryParameters.UserId" path="/summary"/></param>
    /// <param name="period"><inheritdoc cref="GetVideosRequestQueryParameters.Period" path="/summary"/></param>
    /// <param name="sort"><inheritdoc cref="GetVideosRequestQueryParameters.Sort" path="/summary"/></param>
    /// <param name="type"><inheritdoc cref="GetVideosRequestQueryParameters.Type" path="/summary"/></param>
    /// <param name="first"><inheritdoc cref="GetVideosRequestQueryParameters.First" path="/summary"/></param>
    /// <param name="after"><inheritdoc cref="GetVideosRequestQueryParameters.After" path="/summary"/></param>
    /// <param name="before"><inheritdoc cref="GetVideosRequestQueryParameters.Before" path="/summary"/></param>
    public VideoUserQuery(
        string userId,
        VideoQueryPeriod? period = null,
        VideoQuerySort? sort = null,
        VideoQueryType? type = null,
        int? first = null,
        string? after = null,
        string? before = null
        )
        => (
        UserId,
        Period,
        Sort,
        Type,
        First,
        After,
        Before
        ) =
        (
        userId,
        period,
        sort,
        type,
        first,
        after,
        before
        );
}

/// <summary>
/// Get videos made of a specific game or category.
/// </summary>
public record VideoGameQuery
    : GetVideosRequestQueryParameters
{
    /// <summary>
    /// <inheritdoc cref="VideoGameQuery"/>
    /// </summary>
    /// <param name="gameId"><inheritdoc cref="GetVideosRequestQueryParameters.GameId" path="/summary"/></param>
    /// <param name="language"><inheritdoc cref="GetVideosRequestQueryParameters.Language" path="/summary"/></param>
    /// <param name="period"><inheritdoc cref="GetVideosRequestQueryParameters.Period" path="/summary"/></param>
    /// <param name="sort"><inheritdoc cref="GetVideosRequestQueryParameters.Sort" path="/summary"/></param>
    /// <param name="type"><inheritdoc cref="GetVideosRequestQueryParameters.Type" path="/summary"/></param>
    /// <param name="first"><inheritdoc cref="GetVideosRequestQueryParameters.First" path="/summary"/></param>
    public VideoGameQuery(
        string gameId,
        string? language = null,
        VideoQueryPeriod? period = null,
        VideoQuerySort? sort = null,
        VideoQueryType? type = null,
        int? first = null
        )
        => (
        GameId,
        Language,
        Period,
        Sort,
        Type,
        First
        ) =
        (
        gameId,
        language,
        period,
        sort,
        type,
        first
        );
}

/// <summary>
/// Abstract class used to form a <see cref="GetVideosRequest"/>.
/// Use derived classes <see cref="VideoIdQuery"/>, <see cref="VideoUserQuery"/>, and <see cref="VideoGameQuery"/> to create valid queries.
/// </summary>
public abstract record GetVideosRequestQueryParameters
{
    /// <summary>
    /// A list of ids of the videos to get.
    /// You may specify a maximum of 100 IDs. 
    /// The API ignores duplicate IDs and IDs that weren't found (if there's at least one valid ID).
    /// </summary>
    public IEnumerable<string>? Ids { get; protected set; }
    /// <summary>
    /// The user id of the broadcaster whose list of videos you want to get.
    /// </summary>
    public string? UserId { get; protected set; }
    /// <summary>
    /// The id of the game or category you want to get videos for.
    /// </summary>
    public string? GameId { get; protected set; }
    /// <summary>
    /// An ISO 639-1 two-letter code to filter returned videos by.
    /// For a list of supported languages, see <see href="https://help.twitch.tv/s/article/languages-on-twitch#streamlang">Supported Stream Language</see>. 
    /// If the language is not supported, use <c>"other"</c>.
    /// </summary>
    public string? Language { get; protected set; }
    /// <summary>
    /// Filters the returned list of videos by when they were published.
    /// Defaults to <see cref="VideoQueryPeriod.All"/>.
    /// </summary>
    public VideoQueryPeriod? Period { get; protected set; }
    /// <summary>
    /// The sort order to return the videos in.
    /// Defaults to <see cref="VideoQuerySort.Time"/>.
    /// </summary>
    public VideoQuerySort? Sort { get; protected set; }
    /// <summary>
    /// Filters the returned list of videos by type.
    /// Defaults to <see cref="VideoQueryType.All"/>.
    /// </summary>
    public VideoQueryType? Type { get; protected set; }
    /// <summary>
    /// The maximum number of items to return per page in the response. 
    /// The minimum page size is 1 item per page and the maximum is 100. 
    /// The default is 20.
    /// </summary>
    public int? First { get; protected set; }
    /// <summary>
    /// The cursor used to get the next page of results. 
    /// The <see cref="Pagination"/> object in the response contains the cursor’s value.
    /// </summary>
    public string? After { get; protected set; }
    /// <summary>
    /// The cursor used to get the previous page of results. 
    /// The <see cref="Pagination"/> object in the response contains the cursor’s value.
    /// </summary>
    public string? Before { get; protected set; }
}

/// <summary>
/// Contains static definitions for possible video periods to filter a <see cref="GetVideosRequest"/> query by.
/// </summary>
/// <param name="Value">Set a custom value (use only if a corresponding static definition does not exist).</param>
public record VideoQueryPeriod(string Value) 
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// All published videos regardless of publishing time.
    /// </summary>
    public static VideoQueryPeriod All { get; } = new("all");
    /// <summary>
    /// Videos published in the last day.
    /// </summary>
    public static VideoQueryPeriod Day { get; } = new("day");
    /// <summary>
    /// Videos published in the last month.
    /// </summary>
    public static VideoQueryPeriod Month { get; } = new("month");
    /// <summary>
    /// Videos published in the last week.
    /// </summary>
    public static VideoQueryPeriod Week { get; } = new("week");
}

/// <summary>
/// Contains static definitions for possible video sorting methods to order a <see cref="GetVideosRequest"/> response by.
/// </summary>
/// <param name="Value">Set a custom value (use only if a corresponding static definition does not exist).</param>
public record VideoQuerySort(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// Sorts the returned list in descending order by when they were created (i.e., latest video first).
    /// </summary>
    public static VideoQuerySort Time { get; } = new("time");
    /// <summary>
    /// Sorts the returned list in descending order by biggest gains in viewership (i.e., highest trending video first).
    /// </summary>
    public static VideoQuerySort Trending { get; } = new("trending");
    /// <summary>
    /// Sorts the returned list in descending order by most views (i.e., highest number of views first).
    /// </summary>
    public static VideoQuerySort Views { get; } = new("views");
}

/// <summary>
/// Contains static definitions for possible video types to filter a <see cref="GetVideosRequest"/> query by.
/// </summary>
/// <param name="Value">Set a custom value (use only if a corresponding static definition does not exist).</param>
public record VideoQueryType(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// All video types (no filter).
    /// </summary>
    public static VideoQueryType All { get; } = new("all");
    /// <summary>
    /// On-demand videos (VODs) of past streams.
    /// </summary>
    public static VideoQueryType Archive { get; } = new("archive");
    /// <summary>
    /// Highlight reels of past streams.
    /// </summary>
    public static VideoQueryType Highlight { get; } = new("highlight");
    /// <summary>
    /// External videos that the broadcaster uploaded using the Video Producer.
    /// </summary>
    public static VideoQueryType Upload { get; } = new("upload");
}
