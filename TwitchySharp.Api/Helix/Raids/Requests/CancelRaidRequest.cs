using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Raids;
/// <summary>
/// Cancel a pending raid.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#cancel-a-raid">cancel a raid</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageRaids"/>.
/// <para>
/// You can cancel a raid at any point up until the broadcaster clicks Raid Now in the Twitch UX or the 90-second countdown expires.
/// </para>
/// <br/>
/// <b>Rate Limits:</b> You may cancel up to 10 raids within a 10-minute window.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageRaids"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster (channel) to cancel a pending raid for.</param>
public class CancelRaidRequest(
    string clientId,
    string accessToken,
    string broadcasterId
    )
    : HelixApiRequest<CancelRaidResponse>(
        "/raids" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
