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
/// Removes a moderator from the broadcaster’s chat room.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#remove-channel-moderator">remove channel moderator</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageModerators"/>.
/// <b>Rate Limits:</b> A broadcaster may remove a maximum of 10 moderators within a 10-second window.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageModerators"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster (channel) to remove a moderator for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="userId">
/// The user id of the moderator to remove from the broadcaster's channel.
/// </param>
public class RemoveChannelModeratorRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string userId
    )
    : HelixApiRequest<RemoveChannelModeratorResponse>(
        "/moderation/moderators" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("user_id", userId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
