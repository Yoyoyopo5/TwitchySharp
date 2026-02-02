using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Deletes one or more videos.
/// </summary>
/// <remarks>
/// You may delete past broadcasts, highlights, or uploads.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageVideos"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-videos">Delete Videos</see> for more information.
/// </remarks>
public record DeleteVideosRequest
    : TwitchHelixRequest<DeleteVideosResponse>
{
    protected override string Path => "/videos";
    public override HttpMethod Method => HttpMethod.Delete;
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelManageVideos);
    protected override TwitchApiIdentity DefaultIdentity => User;

    /// <summary>
    /// The user identity of the broadcaster who owns the videos to delete.
    /// </summary>
    public required UserIdentity User { get; init; }

    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", Ids.Select(x => x.Value));

    /// <summary>
    /// The ids of the videos to delete.
    /// </summary>
    /// <remarks>
    /// You can delete a maximum of 5 videos per request. Ignores invalid video IDs.
    /// If the user doesn't have permission to delete one of the videos in the list, none of the videos are deleted.
    /// </remarks>
    public required IEnumerable<VideoId> Ids { get; init; }
}
