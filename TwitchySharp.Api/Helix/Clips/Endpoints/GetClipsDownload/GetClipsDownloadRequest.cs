using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

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
    /// <param name="editorId">
    /// The user id of the editor of the channel you want to get clip downloads for.
    /// This must be the user that created the <paramref name="accessToken"/>, and it can be the broadcaster.
    /// </param>
    /// <param name="broadcasterId">The user id of the broadcaster (channel) to get clip donwloads for.</param>
    /// <param name="clipIds">
    /// The id(s) of the clips to get downloads for.
    /// A maximum of 10 clips can be requested at once.
    /// </param>
    public GetClipsDownloadRequest(
        string clientId,
        string accessToken,
        string editorId,
        string broadcasterId,
        IEnumerable<string> clipIds
        )
        : base(
            "/clips/downloads",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("editor_id", editorId)
                .Add("broadcaster_id", broadcasterId)
                .Add("clip_id", clipIds)
            )
    {
        Method = HttpMethod.Get;
    }
}
