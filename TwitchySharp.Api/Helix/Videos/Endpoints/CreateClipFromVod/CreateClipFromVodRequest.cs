using System;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// <b>BETA</b> Creates a clip from a broadcaster’s VOD on behalf of the broadcaster or an editor of the channel.
/// </summary>
/// <remarks>
/// <para>
/// Since a live stream is actively creating a VOD, this endpoint can also be used to create a clip from earlier in the current stream.
/// The <see cref="CreatedVodClip.EditUrl"/> allows you to edit the clip’s title, feature the clip, create a portrait version of the clip, download the clip media, and share the clip directly to social platforms.
/// </para>
/// Requires an app or user access token that includes <see cref="Scope.EditorManageClips"/> or <see cref="Scope.ChannelManageClips"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference#create-clip-from-vod">Create Clip From VOD</see> for more information.
/// </remarks>
public record CreateClipFromVodRequest
    : TwitchHelixRequest<CreateClipFromVodResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token that includes <see cref="Scope.EditorManageClips"/> or <see cref="Scope.ChannelManageClips"/>.</param>
    /// <param name="queryParameters">The parameters of the request.</param>
    public CreateClipFromVodRequest(
        string clientId,
        string accessToken,
        CreateClipFromVodRequestQueryParameters queryParameters
        )
        : base(
            "/videos/clips",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("editor_id", queryParameters.EditorId)
                .Add("broadcaster_id", queryParameters.BroadcasterId)
                .Add("vod_id", queryParameters.VodId)
                .Add("vod_offset", ((int)queryParameters.VodOffset.TotalSeconds).ToString())
                .Add("duration", queryParameters.Duration?.TotalSeconds.ToString())
                .Add("title", queryParameters.Title)
            )
    {
        Method = HttpMethod.Post;
    }
}

/// <summary>
/// Query parameters for a <see cref="CreateClipFromVodRequest"/>.
/// </summary>
public record CreateClipFromVodRequestQueryParameters
{
    /// <summary>
    /// The user id of the editor of the channel to create a clip for.
    /// This should be the same user that created the user access token in the request,
    /// and it can be the broadcaster.
    /// </summary>
    public required string EditorId { get; set; }
    /// <summary>
    /// The user id of the broadcaster (channel) to create a clip for.
    /// </summary>
    public required string BroadcasterId { get; set; }
    /// <summary>
    /// The id of the VOD to create a clip for.
    /// </summary>
    public required string VodId { get; set; }
    /// <summary>
    /// The end time of clip to create, measured from the start of the VOD.
    /// </summary>
    /// <remarks>
    /// The clip will start at <c><see cref="VodOffset"/> - <see cref="Duration"/></c>.
    /// If <see cref="Duration"/> is specified, this must be greater than its value.
    /// </remarks>
    public required TimeSpan VodOffset { get; set; }
    /// <summary>
    /// The duration of the clip to create.
    /// </summary>
    /// <remarks>
    /// Can range from 5 to 60 seconds, with a resolution of 100ms.
    /// If left <see langword="null"/>, defaults to 30 seconds.
    /// </remarks>
    public TimeSpan? Duration { get; set; }
    /// <summary>
    /// The title of the clip to create.
    /// </summary>
    public required string Title { get; set; }
}