using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the broadcaster’s list of custom chat badges. 
/// The list is empty if the broadcaster hasn’t created custom chat badges. 
/// For information about custom badges, see <see href="https://help.twitch.tv/s/article/subscriber-badge-guide">subscriber badges</see> and <see href="https://help.twitch.tv/s/article/custom-bit-badges-guide">Bits badges</see>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="broadcasterId">The user ID of the broadcaster whose chat badges you want to get.</param>
public class GetChannelChatBadgesRequest(string clientId, string accessToken, string broadcasterId)
    : HelixApiRequest<GetChannelChatBadgesResponse>(
        "/chat/badges" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken
        );
