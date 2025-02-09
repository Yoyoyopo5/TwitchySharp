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
/// Raid another channel by sending the broadcaster’s viewers to the targeted channel.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#start-a-raid">start a raid</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageRaids"/>. 
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageRaids"/>.</param>
/// <param name="fromBroadcasterId">
/// The user id of the broadcaster (channel) that is sending the raid.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="toBroadcasterId">The user id of the broadcaster (channel) to send the raid to.</param>
public class StartRaidRequest(
    string clientId,
    string accessToken,
    string fromBroadcasterId,
    string toBroadcasterId
    )
    : HelixApiRequest<StartRaidResponse>(
        "/raids" +
        new HttpQueryParameters()
            .Add("from_broadcaster_id", fromBroadcasterId)
            .Add("to_broadcaster_id", toBroadcasterId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Post;
}
