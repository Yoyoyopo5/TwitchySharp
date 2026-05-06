using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    protected override string Path => "/clips/downloads";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(EditorId),
        ValidScopes = ImmutableHashSet.Create(Scope.EditorManageClips, Scope.ChannelManageClips)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("editor_id", EditorId)
            .Add("broadcaster_id", BroadcasterId)
            .Add("clip_id", ClipIds.Select(x => x.ToString()));

    /// <summary>
    /// The user id of broadcaster or an editor of the channel you want to get clip downloads for.
    /// </summary>
    /// <remarks>
    /// This must be the user that created the access token used in the request.
    /// Requires <see cref="Scope.EditorManageClips"/> or <see cref="Scope.ChannelManageClips"/>.
    /// </remarks>
    public required UserId EditorId { get; init; }

    /// <summary>
    /// The user id of the broadcaster (channel) to get clip downloads for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The id(s) of the clips to get downloads for.
    /// </summary>
    /// <remarks>
    /// A maximum of 10 clips can be requested at once.
    /// </remarks>
    public required IEnumerable<ClipId> ClipIds { get; init; }
}
