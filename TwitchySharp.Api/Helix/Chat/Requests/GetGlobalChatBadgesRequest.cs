using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets Twitch’s list of chat badges, which users may use in any channel’s chat room. 
/// For information about chat badges, see <see href="https://help.twitch.tv/s/article/twitch-chat-badges-guide">Twitch Chat Badges Guide</see>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-global-chat-badges">get global chat badges</see> for more information.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
public class GetGlobalChatBadgesRequest(string clientId, string accessToken)
    : HelixApiRequest<GetGlobalChatBadgesResponse>(
        "/chat/badges/global",
        clientId,
        accessToken
        );
