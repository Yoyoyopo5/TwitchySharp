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
    : TwitchHelixRequest<GetVideosResponse>, IPageableRequest
{
    protected override string Path => "/videos";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", Query.Ids?.Select(x => x.Value))
            .Add("user_id", Query.UserId)
            .Add("game_id", Query.GameId)
            .Add("language", Language)
            .Add("period", Period?.Value)
            .Add("sort", Sort?.Value)
            .Add("type", Type?.Value)
            .Add("first", First?.ToString())
            .Add("after", After?.Value)
            .Add("before", Before?.Value);

    /// <summary>
    /// The query specifying which videos to retrieve.
    /// </summary>
    /// <remarks>
    /// Use <see cref="VideoIdQuery"/>, <see cref="VideoUserQuery"/>, or <see cref="VideoGameQuery"/>.
    /// </remarks>
    public required VideosQuery Query { get; init; }

    /// <summary>
    /// An ISO 639-1 two-letter code to filter returned videos by.
    /// </summary>
    /// <remarks>
    /// Only applicable when querying by game (using <see cref="VideoGameQuery"/>).
    /// For a list of supported languages, see <see href="https://help.twitch.tv/s/article/languages-on-twitch#streamlang">Supported Stream Language</see>.
    /// If the language is not supported, use <see cref="LanguageCode.Other"/>.
    /// </remarks>
    public LanguageCode? Language { get; init; }
    /// <summary>
    /// Filters the returned list of videos by when they were published.
    /// </summary>
    /// <remarks>
    /// Only applicable when querying by user or game (using <see cref="VideoUserQuery"/> or <see cref="VideoGameQuery"/>).
    /// Defaults to <see cref="VideoQueryPeriod.All"/>.
    /// </remarks>
    public VideoQueryPeriod? Period { get; init; }
    /// <summary>
    /// The sort order to return the videos in.
    /// </summary>
    /// <remarks>
    /// Only applicable when querying by user or game (using <see cref="VideoUserQuery"/> or <see cref="VideoGameQuery"/>).
    /// Defaults to <see cref="VideoQuerySort.Time"/>.
    /// </remarks>
    public VideoQuerySort? Sort { get; init; }
    /// <summary>
    /// Filters the returned list of videos by type.
    /// </summary>
    /// <remarks>
    /// Only applicable when querying by user or game (using <see cref="VideoUserQuery"/> or <see cref="VideoGameQuery"/>).
    /// Defaults to <see cref="VideoQueryType.All"/>.
    /// </remarks>
    public VideoQueryType? Type { get; init; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// Only applicable when querying by user or game (using <see cref="VideoUserQuery"/> or <see cref="VideoGameQuery"/>).
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

/// <summary>
/// Base type for videos query parameters.
/// </summary>
/// <remarks>
/// Use derived types <see cref="VideoIdQuery"/>, <see cref="VideoUserQuery"/>, or <see cref="VideoGameQuery"/>.
/// </remarks>
public abstract record VideosQuery
{
    internal IEnumerable<VideoId>? Ids { get; init; }
    internal UserId? UserId { get; init; }
    internal GameId? GameId { get; init; }
}

/// <summary>
/// Query for specific videos by their ids.
/// </summary>
public record VideoIdQuery : VideosQuery
{
    /// <summary>
    /// The video ids to get. Maximum of 100 ids.
    /// </summary>
    /// <remarks>
    /// The API ignores duplicate ids and ids that weren't found (if there's at least one valid id).
    /// </remarks>
    public new required IEnumerable<VideoId> Ids
    {
        get => base.Ids!;
        init => base.Ids = value;
    }
}

/// <summary>
/// Query for videos from a specific user/broadcaster.
/// </summary>
public record VideoUserQuery : VideosQuery
{
    /// <summary>
    /// The user id of the broadcaster whose list of videos you want to get.
    /// </summary>
    public new required UserId UserId
    {
        get => base.UserId!.Value;
        init => base.UserId = value;
    }
}

/// <summary>
/// Query for videos from a specific game or category.
/// </summary>
public record VideoGameQuery : VideosQuery
{
    /// <summary>
    /// The id of the game or category you want to get videos for.
    /// </summary>
    public new required GameId GameId
    {
        get => base.GameId!.Value;
        init => base.GameId = value;
    }
}
