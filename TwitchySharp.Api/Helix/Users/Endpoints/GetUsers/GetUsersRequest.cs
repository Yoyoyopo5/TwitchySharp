using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Gets information about one or more users.
/// </summary>
/// <remarks>
/// You may look up users using their user ID, login name, or both, but the sum total of the number of users you may look up is 100.
/// If you don't specify ids or login names, the request returns information about the user in the access token (if using a user access token).
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
    protected override string Path => "/users";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", UserIds?.Select(x => x.Value))
            .Add("login", UserLogins?.Select(x => x.Value));

    /// <summary>
    /// The ids of the users to get.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 ids total between <see cref="UserIds"/> and <see cref="UserLogins"/>.
    /// </remarks>
    public IEnumerable<UserId>? UserIds { get; set; }
    /// <summary>
    /// The logins (usernames) of the users to get.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 logins total between <see cref="UserIds"/> and <see cref="UserLogins"/>.
    /// </remarks>
    public IEnumerable<UserLogin>? UserLogins { get; set; }
}
