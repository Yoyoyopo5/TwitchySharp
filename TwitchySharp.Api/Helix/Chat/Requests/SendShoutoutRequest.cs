using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Sends a Shoutout to the specified broadcaster. <see href="https://help.twitch.tv/s/article/shoutouts">Learn more</see>.
/// A broadcaster may send a Shoutout once every 2 minutes. They may send the same broadcaster a Shoutout once every 60 minutes.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-a-shoutout">send a shoutout</see> for more information.
/// <br/>
/// Requires a user access token that inlcudes <see cref="Scope.ModeratorManageShoutouts"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageShoutouts"/>.</param>
/// <param name="fromBroadcasterId">The user id of the broadcaster that's sending the shoutout.</param>
/// <param name="toBroadcasterId">The user id of the broadcaster that's receiving the shoutout.</param>
/// <param name="moderatorId">The user id of the sending broadcaster or a moderator of the broadcaster's channel. This must be the same user that created the <paramref name="accessToken"/>.</param>
public class SendShoutoutRequest(string clientId, string accessToken, string fromBroadcasterId, string toBroadcasterId, string moderatorId)
    : HelixApiRequest<SendShoutoutResponse>(
        "/chat/shoutouts" +
        new HttpQueryParameters()
            .Add("from_broadcaster_id", fromBroadcasterId)
            .Add("to_broadcaster_id", toBroadcasterId)
            .Add("moderator_id", moderatorId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Post;
}
