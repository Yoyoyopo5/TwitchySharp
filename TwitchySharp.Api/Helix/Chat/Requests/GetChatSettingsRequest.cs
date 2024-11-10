using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Chat.Requests;
/// <summary>
/// Gets the broadcaster’s chat settings.
/// For an overview of chat settings, see <see href="https://help.twitch.tv/s/article/chat-commands#AllMods">Chat Commands for Broadcasters and Moderators</see> and <see href="https://help.twitch.tv/s/article/setting-up-moderation-for-your-twitch-channel#modpreferences">Moderator Preferences</see>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="broadcasterId">The user id of the broadcaster whose chat settings you want to get.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or one of the broadcaster's moderators.
/// <br/>
/// This parameter is only required if you want to include the <see cref="ChatSettings.NonModeratorChatDelay"/> and <see cref="ChatSettings.NonModeratorChatDelayDuration"/> in the response.
/// <br/>
/// If specified, this must be the same user that created the <paramref name="accessToken"/>.
/// </param>
public class GetChatSettingsRequest(string clientId, string accessToken, string broadcasterId, string? moderatorId = null)
    : HelixApiRequest<GetChatSettingsResponse>(
        "/chat/settings" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId),
        clientId,
        accessToken
        );
