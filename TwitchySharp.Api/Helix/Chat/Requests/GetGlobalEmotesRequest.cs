using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the list of <see href="https://www.twitch.tv/creatorcamp/en/learn-the-basics/emotes/">global emotes</see>. 
/// Global emotes are Twitch-created emotes that users can use in any Twitch chat.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
public class GetGlobalEmotesRequest(string clientId, string accessToken)
    : HelixApiRequest<GetGlobalEmotesResponse>(
        "/chat/emotes/global",
        clientId,
        accessToken
        );
