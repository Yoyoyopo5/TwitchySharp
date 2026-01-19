using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Updates the color used for the user’s name in chat.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserManageChatColor"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-user-chat-color">Update User Chat Color</see> for more information.
/// </remarks>
public record UpdateUserChatColorRequest
    : TwitchHelixRequest<UpdateUserChatColorResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.UserManageChatColor"/>.</param>
    /// <param name="userId">The user id of the user whose color to change. This must be the same user that created the <paramref name="accessToken"/>.</param>
    /// <param name="color">The color to use for the user's name in chat.</param>
    public UpdateUserChatColorRequest(
        string clientId,
        string accessToken,
        string userId,
        ChatColor color
        ) : base(
            "/chat/color",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", userId)
                .Add("color", color)
            )
    {
        Method = HttpMethod.Put;
    }
}