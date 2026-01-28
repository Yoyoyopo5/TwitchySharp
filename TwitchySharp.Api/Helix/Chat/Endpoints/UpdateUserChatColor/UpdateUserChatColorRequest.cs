using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Updates the color used for the user's name in chat.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserManageChatColor"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-user-chat-color">Update User Chat Color</see> for more information.
/// </remarks>
public record UpdateUserChatColorRequest
    : TwitchHelixRequest<UpdateUserChatColorResponse>
{
    protected override string Path => "/chat/color";
    public override HttpMethod Method => HttpMethod.Put;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(UserId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.UserManageChatColor ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("user_id", UserId)
            .Add("color", Color);

    /// <summary>
    /// The user id of the user whose color to change.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.UserManageChatColor"/>.
    /// </remarks>
    public required UserId UserId { get; init; }

    /// <summary>
    /// The color to use for the user's name in chat.
    /// </summary>
    public required ChatColor Color { get; init; }
}
