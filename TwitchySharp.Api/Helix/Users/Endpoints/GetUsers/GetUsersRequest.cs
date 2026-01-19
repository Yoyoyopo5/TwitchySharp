using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Gets information about one or more users.
/// </summary>
/// <remarks>
/// You may look up users using their user ID, login name, or both but the sum total of the number of users you may look up is 100.
/// If you don’t specify ids or login names, the request returns information about the user in the access token (if using a user access token).
/// <para>
/// To include the <see cref="TwitchUser.Email"/> property in the response, the user access token must include <see cref="Scope.UserReadEmail"/> and have been created by the user you want to get an email for.
/// </para>
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-users">Get Users</see> for more information.
/// </remarks>
public record GetUsersRequest
    : TwitchHelixRequest<GetUsersResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="userIds">The ids of the users to get.</param>
    /// <param name="userLogins">The logins (usernames) of the users to get.</param>
    public GetUsersRequest(
        string clientId,
        string accessToken,
        IEnumerable<string>? userIds = null,
        IEnumerable<string>? userLogins = null
        ) : base(
            "/users",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("id", userIds)
                .Add("login", userLogins)
            )
    {
        Method = HttpMethod.Get;
    }
}
