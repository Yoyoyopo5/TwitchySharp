using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Adds a moderator to the broadcaster’s chat room.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#add-channel-moderator">add channel moderator</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageModerators"/>.
/// <br/>
/// <b>Rate Limits:</b> A broadcaster may add a maximum of 10 moderators within a 10-second window.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageModerators"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster (channel) to add a moderator for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="userId">
/// The id of the user to add as a moderator.
/// </param>
public class AddChannelModeratorRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string userId
    )
    : HelixApiRequest<AddChannelModeratorResponse>(
        "/moderation/moderators" + 
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("user_id", userId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Post;
}
