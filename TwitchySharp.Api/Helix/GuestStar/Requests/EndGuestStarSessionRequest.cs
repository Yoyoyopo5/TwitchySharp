using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// BETA
/// <br/>
/// Ends a Guest Star session on behalf of the broadcaster. 
/// Performs the same action as if the host clicked the “End Call” button in the Guest Star UI.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#end-guest-star-session">end Guest Star session</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageGuestStar"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster to end a Guest Star session for.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="sessionId">The id of the Guest Star session to end.</param>
public class EndGuestStarSessionRequest(string clientId, string accessToken, string broadcasterId, string sessionId)
    : HelixApiRequest<EndGuestStarSessionResponse>(
        "/guest_star/session" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("session_id", sessionId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
