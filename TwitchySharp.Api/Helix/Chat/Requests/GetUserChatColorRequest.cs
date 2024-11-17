using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the color used for the user’s name in chat.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-chat-color">get user chat color</see> for more information.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="userIds">
/// The user ids of the users whose username colors you want to get.
/// The maximum number of IDs that you may specify is 100.
/// The API ignores duplicate IDs and IDs that weren’t found.
/// </param>
public class GetUserChatColorRequest(string clientId, string accessToken, IEnumerable<string> userIds)
    : HelixApiRequest<GetUserChatColorResponse>(
        "/chat/color" +
        new HttpQueryParameters()
            .Add("user_id", userIds),
        clientId,
        accessToken
        );
