using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    : TwitchHelixRequest<GetClipsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetClipsRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetClipsRequestParameters? parameters = null
        )
        : base(
            "/clips",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("id", parameters?.Ids?.Select(x => x.ToString()))
                .Add("broadcaster_id", parameters?.BroadcasterId)
                .Add("game_id", parameters?.GameId)
                .Add("started_at", parameters?.StartedAt?.ToUniversalTwitchQueryString())
                .Add("ended_at", parameters?.EndedAt?.ToUniversalTwitchQueryString())
                .Add("first", parameters?.First?.ToString())
                .Add("before", parameters?.Before?.ToString())
                .Add("after", parameters?.After?.ToString())
                .Add("is_featured", parameters?.IsFeatured?.ToString())
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetClipsRequest"/>.
/// </summary>
public record GetClipsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// Get a specific broadcaster's clips.
    /// </summary>
    /// <param name="broadcasterId"><inheritdoc cref="BroadcasterId" path="/summary"/></param>
    public GetClipsRequestParameters(UserId broadcasterId)
        => BroadcasterId = broadcasterId;
    /// <summary>
    /// Get clips of a specific game or category.
    /// </summary>
    /// <param name="gameId"><inheritdoc cref="GameId" path="/summary"/></param>
    public GetClipsRequestParameters(GameId gameId)
        => GameId = gameId;
    /// <summary>
    /// Get a specific clip by its id.
    /// </summary>
    /// <param name="clipIds"><inheritdoc cref="Ids" path="/summary"/></param>
    public GetClipsRequestParameters(IEnumerable<ClipId> clipIds)
         => Ids = clipIds;


    /// <summary>
    /// The user id of the broadcaster whose video clips you want to get.
    /// </summary>
    /// <remarks>
    /// Use this parameter to get clips that were captured from the broadcaster’s streams.
    /// This parameter is mutually exclusive with <see cref="GameId"/> and <see cref="Ids"/>.
    /// </remarks>
    public UserId? BroadcasterId { get; }
    /// <summary>
    /// The id of the game or category whose clips you want to get. 
    /// </summary>
    /// <remarks>
    /// Use this parameter to get clips that were captured from streams that were playing this game.
    /// This parameter is mutually exclusive with <see cref="BroadcasterId"/> and <see cref="Ids"/>.
    /// </remarks>
    public GameId? GameId { get; }
    /// <summary>
    /// The clip id(s) of the clip(s) to get. 
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 ids. 
    /// The API ignores duplicate ids and ids that aren’t found.
    /// This parameter is mutually exclusive with <see cref="BroadcasterId"/> and <see cref="GameId"/>.
    /// </remarks>
    public IEnumerable<ClipId>? Ids { get; }
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
    /// If <see langword="false"/>, returns only clips that aren’t featured. 
    /// All clips are returned if this parameter is <see langword="null"/>.
    /// </remarks>
    public bool? IsFeatured { get; set; }
}