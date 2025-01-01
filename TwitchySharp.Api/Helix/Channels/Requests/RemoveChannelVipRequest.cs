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
/// Removes the specified user as a VIP in the broadcaster’s channel.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#remove-channel-vip">remove channel VIP</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageVips"/>.
/// <br/>
/// <b>Rate Limits:</b> A broadcaster may remove a maximum of 10 VIPs within a 10-second window.
/// <br/>
/// Note that this endpoint can be used to remove VIP status from a user on their behalf. In this case, the <paramref name="accessToken"/> can be created by the user instead of the broadcaster.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageVips"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster (channel) to remove a VIP for.
/// If removing a user's VIP status on behalf of the broadcaster, the broadcaster must have created the <paramref name="accessToken"/>.
/// </param>
/// <param name="userId">
/// The id of the user to revoke VIP status for.
/// If removing this user's VIP status on behalf of the user themselves, this user can have created the <paramref name="accessToken"/>.
/// </param>
public class RemoveChannelVipRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string userId
    )
    : HelixApiRequest<RemoveChannelVipResponse>(
        "/channels/vips" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("user_id", userId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
