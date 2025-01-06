using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Gets information about one or more users.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-users">get users</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <para>
/// You may look up users using their user ID, login name, or both but the sum total of the number of users you may look up is 100.
/// If you don’t specify ids or login names, the request returns information about the user in the access token (if using a user access token).
/// </para>
/// <para>
/// To include the <see cref="TwitchUser.Email"/> property in the response, the user access token must include <see cref="Scope.UserReadEmail"/> and have been created by the user you want to get an email for.
/// </para>
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="userIds">The ids of the users to get.</param>
/// <param name="userLogins">The logins (usernames) of the users to get.</param>
public class GetUsersRequest(
    string clientId,
    string accessToken,
    IEnumerable<string>? userIds = null,
    IEnumerable<string>? userLogins = null
    )
    : HelixApiRequest<GetUsersResponse>(
        "/users" + 
        new HttpQueryParameters()
            .Add("id", userIds)
            .Add("login", userLogins),
        clientId,
        accessToken
        );
