using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Users.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Users.Requests;
/// <summary>
/// Removes the user from the broadcaster’s list of blocked users.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.UserManageBlockedUsers"/>.
/// The user that created the token is who the blocked user is removed for.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#unblock-user">Unblock User</see> for more information.
/// </remarks>
public record UnblockUserRequest
    : TwitchHelixRequest<UnblockUserResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.UserManageBlockedUsers"/>.</param>
    /// <param name="targetUserId">
    /// The id of the user to remove from the broadcaster's list of blocked users.
    /// The API ignores the request if the broadcaster hasn’t blocked the user.
    /// </param>
    public UnblockUserRequest(
        string clientId,
        string accessToken,
        string targetUserId
        ) : base(
            "/users/blocks",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("target_user_id", targetUserId)
            )
    {
        Method = HttpMethod.Delete;
    }
}