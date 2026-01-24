using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Gets the active extensions that the broadcaster has installed for each configuration.
/// </summary>
/// <remarks>
/// <para>
/// Requires an app or user access token.
/// To include extensions that are under development, you must use a user access token that includes <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.UserEditBroadcast"/> .
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-active-extensions">Get User Active Extensions</see> for more information.
/// </remarks>
public record GetUserActiveExtensionsRequest
    : TwitchHelixRequest<GetUserActiveExtensionsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetUserActiveExtensionsRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetUserActiveExtensionsRequestParameters? parameters = null
        ) : base(
            "/users/extensions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("user_id", parameters?.UserId)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetUserActiveExtensionsRequest"/>.
/// </summary>
public record GetUserActiveExtensionsRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster to get active extensions for.
    /// </summary>
    /// <remarks>
    /// Note: Optional only if using a user access token for the access token in the request. In that case, the user that created the token is the one to get extensions for.
    /// </remarks>
    public required UserId? UserId { get; set; }
}
