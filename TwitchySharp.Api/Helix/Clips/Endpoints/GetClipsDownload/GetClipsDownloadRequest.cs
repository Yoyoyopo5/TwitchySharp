using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;
using System.Linq;

namespace TwitchySharp.Api.Helix.Clips;
/// <summary>
/// Provides URLs to download the video file(s) for the specified clips.
/// </summary>
/// <remarks>
/// <b>Rate Limits:</b> Limited to 100 requests per minute.
/// <br/>
/// Requires an app or user access token that includes <see cref="Scope.EditorManageClips"/> or <see cref="Scope.ChannelManageClips"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference#get-clips-download">Get Clips Download</see> for more information.
/// </remarks>
public record GetClipsDownloadRequest
    : TwitchHelixRequest<GetClipsDownloadResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token that includes <see cref="Scope.EditorManageClips"/> or <see cref="Scope.ChannelManageClips"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetClipsDownloadRequest(
        ClientId clientId,
        AccessToken accessToken, // Seems like app token would not work, but docs says its allowed, so I'll leave this general.
        GetClipsDownloadRequestParameters parameters
        )
        : base(
            "/clips/downloads",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("editor_id", parameters.EditorId)
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("clip_id", parameters.ClipIds.Select(x => x.ToString()))
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetClipsDownloadRequest"/>.
/// </summary>
public record GetClipsDownloadRequestParameters
{
    /// <summary>
    /// The user id of broadcaster or an editor of the channel you want to get clip downloads for.
    /// </summary>
    /// <remarks>
    /// This must be the user that created the access token used in the request.
    /// </remarks>
    public required UserId EditorId { get; set; }
    /// <summary>
    /// The user id of the broadcaster (channel) to get clip donwloads for.
    /// </summary>
    public required UserId BroadcasterId { get; set; }
    /// <summary>
    /// The id(s) of the clips to get downloads for.
    /// </summary>
    /// <remarks>
    /// A maximum of 10 clips can be requested at once.
    /// </remarks>
    public required IEnumerable<ClipId> ClipIds { get; set; }
}
