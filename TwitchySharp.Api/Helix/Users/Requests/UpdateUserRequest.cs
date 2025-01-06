using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Updates a user’s description.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-user">update user</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserEdit"/>.
/// To include the user's email address in the response, the token must also include <see cref="Scope.UserReadEmail"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">
/// A user access token that includes <see cref="Scope.UserEdit"/>.
/// The user that created the access token is the one who will be updated.
/// </param>
/// <param name="description">
/// The string to update the channel’s description to. 
/// The description is limited to a maximum of 300 characters.
/// To remove the description, set this to <see cref="string.Empty"/>.
/// </param>
public class UpdateUserRequest(
    string clientId,
    string accessToken,
    string? description
    )
    : HelixApiRequest<UpdateUserResponse>(
        "/users" +
        new HttpQueryParameters()
            .Add("description", HttpUtility.UrlEncode(description)),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Put;
}
