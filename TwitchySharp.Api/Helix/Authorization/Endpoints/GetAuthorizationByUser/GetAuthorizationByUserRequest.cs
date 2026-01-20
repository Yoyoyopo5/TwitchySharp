using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Authorization;
/// <summary>
/// Gets the authorization scopes that the specified user(s) have granted the application.
/// </summary>
/// <remarks>
/// Requires an app access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-authorization-by-user">Get Authorization By User</see> for more information.
/// </remarks>
public record GetAuthorizationByUserRequest
    : TwitchHelixRequest<GetAuthorizationByUserResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app access token.</param>
    /// <param name="userIds">
    /// The user id(s) of the user(s) you want to check authorization for.
    /// A maximum of 10 user ids can be specified.
    /// </param>
    public GetAuthorizationByUserRequest(
        string clientId,
        string accessToken,
        IEnumerable<string> userIds
        )
        : base(
            "/authorization/users",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", userIds)
            )
    {
        Method = HttpMethod.Get;
    }
}