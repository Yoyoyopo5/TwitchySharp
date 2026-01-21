using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    /// <param name="parameters">The request parameters.</param>
    public GetUserChatColorRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetUserChatColorRequestParameters parameters
        )
        : base(
            "/chat/color",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", parameters.UserIds.Select(x => x.ToString()))
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetUserChatColorRequest"/>.
/// </summary>
public record GetUserChatColorRequestParameters
{
    /// <summary>
    /// The user ids of the users whose username colors you want to get.
    /// </summary>
    /// <remarks>
    /// The maximum number of ids that you may specify is 100.
    /// The API ignores duplicate ids and ids that weren’t found.
    /// </remarks>
    public required IEnumerable<UserId> UserIds { get; set; }
}
