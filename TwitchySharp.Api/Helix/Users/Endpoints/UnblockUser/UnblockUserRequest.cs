using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Users;
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
    /// <param name="parameters">The request parameters.</param>
    public UnblockUserRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        UnblockUserRequestParameters parameters
        ) : base(
            "/users/blocks",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("target_user_id", parameters.TargetUserId)
            )
    {
        Method = HttpMethod.Delete;
    }
}

/// <summary>
/// Request parameters for a <see cref="UnblockUserRequest"/>.
/// </summary>
public record UnblockUserRequestParameters
{
    /// <summary>
    /// The id of the user to remove from the broadcaster's list of blocked users.
    /// </summary>
    /// <remarks>
    /// The API ignores the request if the broadcaster hasn’t blocked the user.
    /// </remarks>
    public required UserId TargetUserId { get; set; }
}