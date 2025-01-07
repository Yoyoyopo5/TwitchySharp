using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Gets the active extensions that the broadcaster has installed for each configuration.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-active-extensions">get user active extensions</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// To include extensions that are under development, you must use a user access token that includes <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.UserEditBroadcast"/> and was created by the <paramref name="userId"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="userId">
/// The user id of the broadcaster to get active extensions for.
/// <para>
/// Note: Optional only if using a user access token for <paramref name="accessToken"/>. In that case, the user that created the token is the one to get extensions for.
/// </para>
/// </param>
public class GetUserActiveExtensionsRequest(
    string clientId,
    string accessToken,
    string? userId
    )
    : HelixApiRequest<GetUserActiveExtensionsResponse>(
        "/users/extensions" +
        new HttpQueryParameters()
            .Add("user_id", userId),
        clientId,
        accessToken
        );
