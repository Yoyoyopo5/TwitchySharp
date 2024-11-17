using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Clips;
/// <summary>
/// Gets one or more video clips that were captured from streams. For information about clips, see <see href="https://help.twitch.tv/s/article/how-to-use-clips">How to use clips</see>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-clips">get clips</see> for more information.
/// </summary>
/// <remarks>
/// The <paramref name="id"/>, <paramref name="gameId"/>, and <paramref name="broadcasterId"/> parameters are mutually exclusive.
/// <br/>
/// Requires an app or user access token.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="broadcasterId"> 	
/// The user id of the broadcaster whose video clips you want to get. 
/// Use this parameter to get clips that were captured from the broadcaster’s streams.
/// This parameter is mutually exclusive with <paramref name="gameId"/> and <paramref name="id"/>.
/// </param>
/// <param name="gameId">
/// The game id of the game whose clips you want to get. 
/// Use this parameter to get clips that were captured from streams that were playing this game.
/// This parameter is mutually exclusive with <paramref name="broadcasterId"/> and <paramref name="id"/>.
/// </param>
/// <param name="id">
/// The clip id of the clip to get. 
/// You may specify a maximum of 100 IDs. 
/// The API ignores duplicate IDs and IDs that aren’t found.
/// This parameter is mutually exclusive with <paramref name="broadcasterId"/> and <paramref name="gameId"/>.
/// </param>
/// <param name="startedAt">
/// The start date used to filter clips. 
/// The API returns only clips within the start and end date window.
/// </param>
/// <param name="endedAt">
/// The end date used to filter clips. 
/// If not specified, the time window is the start date plus one week.
/// </param>
/// <param name="first">
/// The maximum number of clips to return per page in the response. 
/// The minimum page size is 1 clip per page and the maximum is 100. 
/// The default is 20.
/// </param>
/// <param name="before">
/// The cursor used to get the previous page of results. 
/// The <see cref="GetClipsResponse.Pagination"/> property in the response contains the cursor’s value. 
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="GetClipsResponse.Pagination"/> property in the response contains the cursor’s value.
/// </param>
/// <param name="isFeatured">
/// Determines whether the response includes featured clips. 
/// If <see langword="true"/>, returns only clips that are featured. 
/// If <see langword="false"/>, returns only clips that aren’t featured. 
/// All clips are returned if this parameter is <see langword="null"/>.
/// </param>
public class GetClipsRequest(
    string clientId, 
    string accessToken, 
    string? broadcasterId = null,
    string? gameId = null,
    string? id = null,
    DateTimeOffset? startedAt = null,
    DateTimeOffset? endedAt = null,
    int? first = null,
    string? before = null,
    string? after = null,
    bool? isFeatured = null
    )
    : HelixApiRequest<GetClipsResponse>(
        "/clips" +
        new HttpQueryParameters()
            .Add("id", id)
            .Add("broadcaster_id", id is null ? broadcasterId : null)
            .Add("game_id", id is null && broadcasterId is null ? gameId : null) // Should we remove the mutual exclusivity guard and allow the API to return the error if more than one is set? This may be unexpected behavior.
            .Add("started_at", startedAt?.UtcDateTime.Date.ToString("yyyy-MM-dd'T'HH:mm:ssZ"))
            .Add("ended_at", endedAt?.UtcDateTime.Date.ToString("yyyy-MM-dd'T'HH:mm:ssZ"))
            .Add("first", first?.ToString())
            .Add("before", before)
            .Add("after", after)
            .Add("is_featured", isFeatured?.ToString()),
        clientId,
        accessToken
        );