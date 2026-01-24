using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Gets a list of markers from the user’s most recent stream or from the specified VOD/video. 
/// </summary>
/// <remarks>
/// A marker is an arbitrary point in a live stream that the broadcaster or editor marked, so they can return to that spot later to create video highlights (see Video Producer, Highlights in the Twitch UX).
/// <br/>
/// Requires a user access token that includes <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.ChannelManageBroadcast"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-stream-markers">Get Stream Markers</see> for more information.
/// </remarks>
public record GetStreamMarkersRequest
    : TwitchHelixRequest<GetStreamMarkersResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.ChannelManageBroadcast"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetStreamMarkersRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetStreamMarkersRequestParameters parameters
        ) : base(
            "/streams/markers",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", parameters.UserId)
                .Add("video_id", parameters.VideoId)
                .Add("first", parameters.First?.ToString())
                .Add("before", parameters.Before?.Value)
                .Add("after", parameters.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Used to query for markers on a specific broadcaster's latest video.
/// </summary>
public record BroadcasterStreamMarkersQuery
    : GetStreamMarkersRequestParameters
{
    /// <summary>
    /// <inheritdoc cref="BroadcasterStreamMarkersQuery"/>
    /// </summary>
    /// <param name="userId">
    /// <inheritdoc cref="GetStreamMarkersRequestParameters.UserId" path="/summary"/>
    /// </param>
    public BroadcasterStreamMarkersQuery(UserId userId)
        => UserId = userId;
}

/// <summary>
/// Used to query for markers on a specific video.
/// </summary>
public record VideoStreamMarkersQuery
    : GetStreamMarkersRequestParameters
{
    /// <summary>
    /// <inheritdoc cref="VideoStreamMarkersQuery"/>
    /// </summary>
    /// <param name="videoId">
    /// <inheritdoc cref="GetStreamMarkersRequestParameters.VideoId" path="/summary"/>
    /// </param>
    public VideoStreamMarkersQuery(VideoId videoId)
        => VideoId = videoId;
}

/// <summary>
/// Request parameters for a <see cref="GetStreamMarkersRequest"/>.
/// Use derived classes <see cref="BroadcasterStreamMarkersQuery"/> and <see cref="VideoStreamMarkersQuery"/> to obey mutually exclusivity rules.
/// </summary>
public record GetStreamMarkersRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The user id of the broadcaster to get markers for.
    /// If set, the request will return markers from this user’s most recent video. 
    /// This user or one of this broadcaster's editors must have created the user access token used in the <see cref="GetStreamMarkersRequest"/>.
    /// </summary>
    public UserId? UserId { get; protected set; }
    /// <summary>
    /// The video id of the video to get markers for.
    /// If set, the request will return marks from this specific video.
    /// The broadcaster who created the video or one of the broadcaster's editors must have created the user access token used in the <see cref="GetStreamMarkersRequest"/>.
    /// </summary>
    public VideoId? VideoId { get; protected set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page. 
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    public PaginationCursor? After { get; set; }
    /// <summary>
    /// The cursor of the result to get results before.
    /// </summary>
    public PaginationCursor? Before { get; set; }
    protected GetStreamMarkersRequestParameters() { }
}
