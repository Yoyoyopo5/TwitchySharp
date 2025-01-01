using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Adds the specified user as a VIP in the broadcaster’s channel.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#add-channel-vip">add channel VIP</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageVips"/>.
/// <br/>
/// <b>Rate Limits:</b> A broadcaster may add a maximum of 10 VIPs within a 10-second window.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageVips"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster (channel) to add a VIP for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="userId">The id of the user to give VIP status to.</param>
public class AddChannelVipRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string userId
    )
    : HelixApiRequest<AddChannelVipResponse>(
        "/channels/vips" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("user_id", userId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Post;
}
