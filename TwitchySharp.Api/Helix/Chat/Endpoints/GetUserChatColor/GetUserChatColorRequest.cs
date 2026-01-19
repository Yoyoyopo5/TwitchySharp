using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the color used for the user’s name in chat.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-chat-color">Get User Chat Color</see> for more information.
/// </remarks>
public record GetUserChatColorRequest
    : TwitchHelixRequest<GetUserChatColorResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="userIds">
    /// The user ids of the users whose username colors you want to get.
    /// The maximum number of IDs that you may specify is 100.
    /// The API ignores duplicate IDs and IDs that weren’t found.
    /// </param>
    public GetUserChatColorRequest(
        string clientId,
        string accessToken,
        IEnumerable<string> userIds
        )
        : base(
            "/chat/color",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", userIds)
            )
    {
        Method = HttpMethod.Get;
    }
}
