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
/// Creates a Guest Star session on behalf of the broadcaster. 
/// Requires the broadcaster to be present in the call interface, or the call will be ended automatically.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-guest-star-session">create Guest Star session</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageGuestStar"/>.</param>
/// <param name="broadcasterId">
/// The user id of the broadcaster to create a Guest Star session for.
/// This must be the same user who created the <paramref name="accessToken"/>.
/// </param>
public class CreateGuestStarSessionRequest(string clientId, string accessToken, string broadcasterId)
    : HelixApiRequest<CreateGuestStarSessionResponse>(
        "/guest_star/session" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Post;
}
