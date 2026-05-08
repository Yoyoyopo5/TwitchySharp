using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Gets information about one or more users.
/// </summary>
/// <remarks>
/// You may look up users using their user ID, login name, or both, but the sum total of the number of users you may look up is 100.
/// If you don't specify ids or login names, the request returns information about the user in the access token (if using a user access token).
/// <para>
/// To include the <see cref="TwitchUser.Email"/> property in the response, use <see cref="IncludingEmailFor"/>
/// which requires a user access token with <see cref="Scope.UserReadEmail"/> created by the user being queried.
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
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = UserIdentity ?? TwitchIdentity.Client.Default,
        ValidScopes = ConfiguredScopes
    };
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
    public IEnumerable<UserId>? UserIds { get; init; }
    /// <summary>
    /// The logins (usernames) of the users to get.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 logins total between <see cref="UserIds"/> and <see cref="UserLogins"/>.
    /// </remarks>
    public IEnumerable<UserLogin>? UserLogins { get; init; }

    /// <summary>
    /// Returns a new request configured to include the user's email in the response.
    /// </summary>
    /// <remarks>
    /// The email is only included if the token was created by the user being queried
    /// and includes <see cref="Scope.UserReadEmail"/>.
    /// If <see cref="UserIds"/> and <see cref="UserLogins"/> are empty, the request will
    /// return information about the token owner including their email.
    /// </remarks>
    /// <param name="user">The user identity to fetch email for.</param>
    /// <returns>A new <see cref="GetUsersRequest"/> configured for email access.</returns>
    public GetUsersRequest IncludingEmailFor(TwitchIdentity.User user)
        => this with { UserIdentity = user, ConfiguredScopes = ImmutableHashSet.Create(Scope.UserReadEmail) };
    private TwitchIdentity.User? UserIdentity { get; init; }
    private IReadOnlySet<Scope> ConfiguredScopes { get; init; } = ImmutableHashSet<Scope>.Empty;
}
