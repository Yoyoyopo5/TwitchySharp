using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", Ids?.Select(x => x.ToString()))
            .Add("broadcaster_id", BroadcasterId)
            .Add("game_id", GameId)
            .Add("started_at", StartedAt?.ToUniversalTwitchQueryString())
            .Add("ended_at", EndedAt?.ToUniversalTwitchQueryString())
            .Add("first", First?.ToString())
            .Add("before", Before?.ToString())
            .Add("after", After?.ToString())
            .Add("is_featured", IsFeatured?.ToString());

    /// <summary>
    /// The user id of the broadcaster whose video clips you want to get.
    /// </summary>
    /// <remarks>
    /// Use this parameter to get clips that were captured from the broadcaster's streams.
    /// This parameter is mutually exclusive with <see cref="GameId"/> and <see cref="Ids"/>.
    /// At least one of <see cref="BroadcasterId"/>, <see cref="GameId"/>, or <see cref="Ids"/> should be specified.
    /// </remarks>
    public UserId? BroadcasterId { get; set; }

    /// <summary>
    /// The id of the game or category whose clips you want to get.
    /// </summary>
    /// <remarks>
    /// Use this parameter to get clips that were captured from streams that were playing this game.
    /// This parameter is mutually exclusive with <see cref="BroadcasterId"/> and <see cref="Ids"/>.
    /// At least one of <see cref="BroadcasterId"/>, <see cref="GameId"/>, or <see cref="Ids"/> should be specified.
    /// </remarks>
    public GameId? GameId { get; set; }

    /// <summary>
    /// The clip id(s) of the clip(s) to get.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 ids.
    /// The API ignores duplicate ids and ids that aren't found.
    /// This parameter is mutually exclusive with <see cref="BroadcasterId"/> and <see cref="GameId"/>.
    /// At least one of <see cref="BroadcasterId"/>, <see cref="GameId"/>, or <see cref="Ids"/> should be specified.
    /// </remarks>
    public IEnumerable<ClipId>? Ids { get; set; }

    /// <summary>
    /// The start date used to filter clips.
    /// </summary>
    /// <remarks>
    /// The API returns only clips within the start and end date window.
    /// </remarks>
    public DateTimeOffset? StartedAt { get; set; }

    /// <summary>
    /// The end date used to filter clips.
    /// </summary>
    /// <remarks>
    /// If <see langword="null"/>, the time window is <see cref="StartedAt"/> plus one week.
    /// </remarks>
    public DateTimeOffset? EndedAt { get; set; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 clip per page and the maximum is 100.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; set; }

    /// <summary>
    /// The cursor of the result to get results before.
    /// </summary>
    public PaginationCursor? Before { get; set; }

    /// <summary>
    /// Determines whether the response includes featured clips.
    /// </summary>
    /// <remarks>
    /// If <see langword="true"/>, returns only clips that are featured.
    /// If <see langword="false"/>, returns only clips that aren't featured.
    /// All clips are returned if this parameter is <see langword="null"/>.
    /// </remarks>
    public bool? IsFeatured { get; set; }
}
