using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Gets a list of markers from the user’s most recent stream or from the specified VOD/video. 
/// A marker is an arbitrary point in a live stream that the broadcaster or editor marked, so they can return to that spot later to create video highlights (see Video Producer, Highlights in the Twitch UX).
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-stream-markers">get stream markers</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.ChannelManageBroadcast"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.ChannelManageBroadcast"/>.</param>
/// <param name="query">The broadcaster or video to get markers for.</param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 100 items per page. 
/// The default is 20.
/// </param>
/// <param name="before">
/// The cursor used to get the previous page of results. 
/// The <see cref="Pagination"/> object in the response contains the cursor’s value.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> object in the response contains the cursor’s value.
/// </param>
public class GetStreamMarkersRequest(
    string clientId,
    string accessToken,
    StreamMarkersQuery query,
    int? first = null,
    string? before = null,
    string? after = null
    )
    : HelixApiRequest<GetStreamMarkersResponse>(
        "/streams/markers" + 
        new HttpQueryParameters()
            .Add("user_id", query.UserId)
            .Add("video_id", query.VideoId)
            .Add("first", first?.ToString())
            .Add("before", before)
            .Add("after", after),
        clientId,
        accessToken
        );

/// <summary>
/// Used to query for markers on a specific broadcaster's latest video.
/// </summary>
public record BroadcasterStreamMarkersQuery
    : StreamMarkersQuery
{
    /// <summary>
    /// <inheritdoc cref="BroadcasterStreamMarkersQuery"/>
    /// </summary>
    /// <param name="userId">
    /// <inheritdoc cref="StreamMarkersQuery.UserId" path="/summary"/>
    /// </param>
    public BroadcasterStreamMarkersQuery(string userId)
        => UserId = userId;
}

/// <summary>
/// Used to query for markers on a specific video.
/// </summary>
public record VideoStreamMarkersQuery
    : StreamMarkersQuery
{
    /// <summary>
    /// <inheritdoc cref="VideoStreamMarkersQuery"/>
    /// </summary>
    /// <param name="videoId">
    /// <inheritdoc cref="StreamMarkersQuery.VideoId" path="/summary"/>
    /// </param>
    public VideoStreamMarkersQuery(string videoId)
        => VideoId = videoId;
}

/// <summary>
/// Used to set a query for a <see cref="GetStreamMarkersRequest"/>.
/// Use derived classes <see cref="BroadcasterStreamMarkersQuery"/> and <see cref="VideoStreamMarkersQuery"/> to obey mutually exclusivity rules.
/// </summary>
public record StreamMarkersQuery
{
    /// <summary>
    /// The user id of the broadcaster to get markers for.
    /// If set, the request will return markers from this user’s most recent video. 
    /// This user or one of this broadcaster's editors must have created the user access token used in the <see cref="GetStreamMarkersRequest"/>.
    /// </summary>
    public string? UserId { get; protected set; }
    /// <summary>
    /// The video id of the video to get markers for.
    /// If set, the request will return marks from this specific video.
    /// The broadcaster who created the video or one of the broadcaster's editors must have created the user access token used in the <see cref="GetStreamMarkersRequest"/>.
    /// </summary>
    public string? VideoId { get; protected set; }
    protected StreamMarkersQuery() { }
}
