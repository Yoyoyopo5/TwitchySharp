using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    /// <param name="parameters">The request parameters.</param>
    public UpdateUserChatColorRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        UpdateUserChatColorRequestParameters parameters
        ) : base(
            "/chat/color",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", parameters.UserId)
                .Add("color", parameters.Color)
            )
    {
        Method = HttpMethod.Put;
    }
}

/// <summary>
/// Request parameters for an <see cref="UpdateUserChatColorRequest"/>.
/// </summary>
public record UpdateUserChatColorRequestParameters
{
    /// <summary>
    /// The user id of the user whose color to change.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId UserId { get; set; }
    /// <summary>
    /// The color to use for the user's name in chat.
    /// </summary>
    public required ChatColor Color { get; set; }
}