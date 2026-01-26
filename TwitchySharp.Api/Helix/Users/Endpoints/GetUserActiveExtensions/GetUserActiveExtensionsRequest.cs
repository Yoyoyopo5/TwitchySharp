using System.Collections.Generic;
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
/// To include extensions that are under development, you must use a user access token that includes <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.UserEditBroadcast"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-active-extensions">Get User Active Extensions</see> for more information.
/// </remarks>
public record GetUserActiveExtensionsRequest
    : TwitchHelixRequest<GetUserActiveExtensionsResponse>
{
    protected override string Path => "/users/extensions";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [Scope.UserReadBroadcast, Scope.UserEditBroadcast];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("user_id", UserId);

    /// <summary>
    /// The user id of the broadcaster to get active extensions for.
    /// </summary>
    /// <remarks>
    /// Optional only if using a user access token. In that case, the user that created the token is the one to get extensions for.
    /// </remarks>
    public UserId? UserId { get; set; }
}
