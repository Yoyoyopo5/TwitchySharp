using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Gets the broadcaster’s AutoMod settings. 
/// The settings are used to automatically block inappropriate or harassing messages from appearing in the broadcaster’s chat room.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-automod-settings">get AutoMod settings</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorReadAutomodSettings"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorReadAutomodSettings"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster (channel) to get AutoMod settings for.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
public class GetAutoModSettingsRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId
    )
    : HelixApiRequest<GetAutoModSettingsResponse>(
        "/moderation/automod/settings" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId),
        clientId,
        accessToken
        );
