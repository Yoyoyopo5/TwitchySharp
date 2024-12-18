using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b>
/// <br/>
/// Gets the channel settings for configuration of the Guest Star feature for a particular host.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-guest-star-settings">get channel guest star settings</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes one of <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes one of <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster to get Guest Star settings for.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator in the broadcaster's chat.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
public class GetChannelGuestStarSettingsRequest(string clientId, string accessToken, string broadcasterId, string moderatorId)
    : HelixApiRequest<GetChannelGuestStarSettingsResponse>(
        "/guest_star/channel_settings" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId),
        clientId,
        accessToken
        );
