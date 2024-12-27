using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// BETA
/// <br/>
/// Gets information about an ongoing Guest Star session for a particular channel.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-guest-star-session">get Guest Star session</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes one of <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes one of <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster hosting the Guest Star session.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator in the broadcaster's chat.
/// This user must be the one that created the <paramref name="accessToken"/>.
/// </param>
public class GetGuestStarSessionRequest(string clientId, string accessToken, string broadcasterId, string moderatorId)
    : HelixApiRequest<GetGuestStarSessionResponse>(
        "/guest_star/session" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId),
        clientId,
        accessToken
        );
