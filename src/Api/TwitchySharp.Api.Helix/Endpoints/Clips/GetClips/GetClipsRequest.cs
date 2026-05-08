using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.Clips;
/// <summary>
/// Gets one or more video clips that were captured from streams.
/// </summary>
/// <remarks>
/// For information about clips, see <see href="https://help.twitch.tv/s/article/how-to-use-clips">How to use clips</see>.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-clips">Get Clips</see> for more information.
/// </remarks>
public record GetClipsRequest
    : TwitchHelixRequest<GetClipsResponse>, IPageableRequest
{
    protected override string Path => "/clips";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", Query.Ids?.Select(x => x.ToString()))
            .Add("broadcaster_id", Query.BroadcasterId)
            .Add("game_id", Query.GameId)
            .Add("started_at", StartedAt?.UtcDateTime.ToRfc3339())
            .Add("ended_at", EndedAt?.UtcDateTime.ToRfc3339())
            .Add("first", First?.ToString())
            .Add("before", Before?.ToString())
            .Add("after", After?.ToString())
            .Add("is_featured", IsFeatured?.ToString());

    /// <summary>
    /// The query specifying which clips to retrieve.
    /// </summary>
    /// <remarks>
    /// Use <see cref="BroadcasterClipsQuery"/>, <see cref="GameClipsQuery"/>, or <see cref="ClipsIdQuery"/>.
    /// </remarks>
    public required ClipsQuery Query { get; init; }

    /// <summary>
    /// The start date used to filter clips.
    /// </summary>
    /// <remarks>
    /// The API returns only clips within the start and end date window.
    /// </remarks>
    public DateTimeOffset? StartedAt { get; init; }

    /// <summary>
    /// The end date used to filter clips.
    /// </summary>
    /// <remarks>
    /// If <see langword="null"/>, the time window is <see cref="StartedAt"/> plus one week.
    /// </remarks>
    public DateTimeOffset? EndedAt { get; init; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 clip per page and the maximum is 100.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }

    /// <summary>
    /// The cursor of the result to get results before.
    /// </summary>
    public PaginationCursor? Before { get; init; }

    /// <summary>
    /// Determines whether the response includes featured clips.
    /// </summary>
    /// <remarks>
    /// If <see langword="true"/>, returns only clips that are featured.
    /// If <see langword="false"/>, returns only clips that aren't featured.
    /// All clips are returned if this parameter is <see langword="null"/>.
    /// </remarks>
    public bool? IsFeatured { get; init; }
}

/// <summary>
/// Base type for clips query parameters.
/// </summary>
/// <remarks>
/// Use derived types <see cref="BroadcasterClipsQuery"/>, <see cref="GameClipsQuery"/>, or <see cref="ClipsIdQuery"/>.
/// </remarks>
public abstract record ClipsQuery
{
    internal UserId? BroadcasterId { get; init; }
    internal GameId? GameId { get; init; }
    internal IEnumerable<ClipId>? Ids { get; init; }
}

/// <summary>
/// Query for clips from a specific broadcaster's streams.
/// </summary>
public record BroadcasterClipsQuery : ClipsQuery
{
    /// <summary>
    /// The user id of the broadcaster whose clips you want to get.
    /// </summary>
    public new required UserId BroadcasterId
    {
        get => base.BroadcasterId!.Value;
        init => base.BroadcasterId = value;
    }
}

/// <summary>
/// Query for clips from streams playing a specific game or category.
/// </summary>
public record GameClipsQuery : ClipsQuery
{
    /// <summary>
    /// The id of the game or category whose clips you want to get.
    /// </summary>
    public new required GameId GameId
    {
        get => base.GameId!.Value;
        init => base.GameId = value;
    }
}

/// <summary>
/// Query for specific clips by their ids.
/// </summary>
public record ClipsIdQuery : ClipsQuery
{
    /// <summary>
    /// The clip ids to get. Maximum of 100 ids.
    /// </summary>
    public new required IEnumerable<ClipId> Ids
    {
        get => base.Ids!;
        init => base.Ids = value;
    }
}
