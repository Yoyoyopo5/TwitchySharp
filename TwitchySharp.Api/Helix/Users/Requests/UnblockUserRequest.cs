using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Removes the user from the broadcaster’s list of blocked users.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#unblock-user">unblock user</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserManageBlockedUsers"/>.
/// The user that created the token is who the blocked user is removed for.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.UserManageBlockedUsers"/>.</param>
/// <param name="targetUserId">
/// The id of the user to remove from the broadcaster's list of blocked users.
/// The API ignores the request if the broadcaster hasn’t blocked the user.
/// </param>
public class UnblockUserRequest(
    string clientId,
    string accessToken,
    string targetUserId
    ) 
    : HelixApiRequest<UnblockUserResponse>(
        "/users/blocks" +
        new HttpQueryParameters()
            .Add("target_user_id", targetUserId),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Delete;
}
