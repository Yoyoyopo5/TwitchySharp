using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Gets a list of markers from the user's most recent stream or from the specified VOD/video.
/// </summary>
/// <remarks>
/// A marker is an arbitrary point in a live stream that the broadcaster or editor marked, so they can return to that spot later to create video highlights (see Video Producer, Highlights in the Twitch UX).
/// <br/>
/// Requires a user access token that includes <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.ChannelManageBroadcast"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-stream-markers">Get Stream Markers</see> for more information.
/// </remarks>
public record GetStreamMarkersRequest
    : TwitchHelixRequest<GetStreamMarkersResponse>, IPageableRequest
{
    protected override string Path => "/streams/markers";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => User;
    public override IEnumerable<Scope> ValidScopes => [ Scope.UserReadBroadcast, Scope.ChannelManageBroadcast ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("user_id", UserId)
            .Add("video_id", VideoId)
            .Add("first", First?.ToString())
            .Add("before", Before?.Value)
            .Add("after", After?.Value);

    /// <summary>
    /// The user to get stream markers as (broadcaster or editor).
    /// </summary>
    public required UserIdentity User { get; init; }

    /// <summary>
    /// The user id of the broadcaster to get markers for.
    /// If set, the request will return markers from this user's most recent video.
    /// This user or one of this broadcaster's editors must have created the user access token used in the request.
    /// </summary>
    /// <remarks>
    /// Mutually exclusive with <see cref="VideoId"/>. One of <see cref="UserId"/> or <see cref="VideoId"/> must be set.
    /// </remarks>
    public UserId? UserId { get; init; }
    /// <summary>
    /// The video id of the video to get markers for.
    /// If set, the request will return marks from this specific video.
    /// The broadcaster who created the video or one of the broadcaster's editors must have created the user access token used in the request.
    /// </summary>
    /// <remarks>
    /// Mutually exclusive with <see cref="UserId"/>. One of <see cref="UserId"/> or <see cref="VideoId"/> must be set.
    /// </remarks>
    public VideoId? VideoId { get; init; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100 items per page.
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
