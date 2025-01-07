using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Gets a list of all extensions (both active and inactive) that a broadcaster has installed.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-extensions">get user extensions</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.UserEditBroadcast"/>. 
/// <see cref="Scope.UserEditBroadcast"/> is required to get inactive extensions.
/// The user who created the token is the broadcaster to get extensions for.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">
/// A user access token that includes <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.UserEditBroadcast"/>.
/// The user who created the token is the broadcaster to get extensions for.
/// </param>
public class GetUserExtensionsRequest(
    string clientId,
    string accessToken
    )
    : HelixApiRequest<GetUserExtensionsResponse>(
        "/users/extensions/list",
        clientId,
        accessToken
        );
