using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Deletes one or more videos. 
/// You may delete past broadcasts, highlights, or uploads.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-videos">delete videos</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageVideos"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageVideos"/>.</param>
/// <param name="ids">
/// The ids of the videos to delete.
/// You can delete a maximum of 5 videos per request. Ignores invalid video IDs.
/// If the user doesn’t have permission to delete one of the videos in the list, none of the videos are deleted.
/// </param>
public class DeleteVideosRequest(
    string clientId,
    string accessToken,
    IEnumerable<string> ids
    )
    : HelixApiRequest<DeleteVideosResponse>(
        "/videos" +
        new HttpQueryParameters()
            .Add("id", ids),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
