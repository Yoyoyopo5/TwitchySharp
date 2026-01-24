using System.Collections.Generic;
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
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageVideos"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public DeleteVideosRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        DeleteVideosRequestParameters parameters
        ) : base(
            "/videos",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("id", parameters.Ids.Select(x => x.Value))
            )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="DeleteVideosRequest"/>.
/// </summary>
public record DeleteVideosRequestParameters
{
    /// <summary>
    /// The ids of the videos to delete.
    /// </summary>
    /// <remarks>
    /// You can delete a maximum of 5 videos per request. Ignores invalid video IDs.
    /// If the user doesn’t have permission to delete one of the videos in the list, none of the videos are deleted.
    /// </remarks>
    public required IEnumerable<VideoId> Ids { get; set; }
}
