using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

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
    /// <param name="parameters">The request parameters.</param>
    public GetAuthorizationByUserRequest(
        ClientId clientId,
        AppAccessToken accessToken,
        GetAuthorizationByUserRequestParameters parameters
        )
        : base(
            "/authorization/users",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", parameters.UserIds.Select(x => x.Value))
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetAuthorizationByUserRequest"/>.
/// </summary>
public record GetAuthorizationByUserRequestParameters
{
    /// <summary>
    /// The user id(s) of the user(s) you want to check authorization for.
    /// </summary>
    /// <remarks>
    /// A maximum of 10 user ids can be specified.
    /// </remarks>
    public required IEnumerable<UserId> UserIds { get; set; }
}