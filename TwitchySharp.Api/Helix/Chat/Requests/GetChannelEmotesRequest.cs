using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the broadcaster’s list of custom emotes. 
/// Broadcasters create these custom emotes for users who subscribe to or follow the channel or cheer Bits in the channel’s chat window.
/// For information about the custom emotes, see <see href="https://help.twitch.tv/s/article/subscriber-emote-guide">subscriber emotes</see>, <see href="https://help.twitch.tv/s/article/custom-bit-badges-guide?language=bg#slots">Bits tier emotes</see>, and <see href="https://blog.twitch.tv/en/2021/06/04/kicking-off-10-years-with-our-biggest-emote-update-ever/">follower emotes</see>.
/// <br/>
/// <b>NOTE:</b> With the exception of custom follower emotes, users may use custom emotes in any Twitch chat.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="broadcasterId">The user id of the broadcaster whose emotes you want to get.</param>
public class GetChannelEmotesRequest(string clientId, string accessToken, string broadcasterId)
    : HelixApiRequest<GetChannelEmotesResponse>(
        "/chat/emotes" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId),
        clientId,
        accessToken
        );
