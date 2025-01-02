using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets the broadcaster’s Shield Mode activation status.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-shield-mode-status">get Shield Mode status</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadShieldMode"/> or <see cref="Scope.ModeratorManageShieldMode"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorReadShieldMode"/> or <see cref="Scope.ModeratorManageShieldMode"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster (channel) to get Shield Mode status for.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
public class GetShieldModeStatusRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId
    )
    : HelixApiRequest<GetShieldModeStatusResponse>(
        "/moderation/shield_mode" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId),
        clientId,
        accessToken
        );
