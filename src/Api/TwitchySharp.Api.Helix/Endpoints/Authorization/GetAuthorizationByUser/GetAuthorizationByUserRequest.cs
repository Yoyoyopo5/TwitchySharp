namespace TwitchySharp.Api.Helix.Authorization;
/// <summary>
/// Gets the authorization scopes that the specified user(s) have granted the application.
/// </summary>
/// <remarks>
/// <para>
/// Requires an app access token.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-authorization-by-user">Get Authorization By User</see> for more information.
/// </remarks>
public record GetAuthorizationByUserRequest
    : TwitchHelixRequest<GetAuthorizationByUserResponseContent>,
    IAuthenticatedTwitchRequest<TwitchRequestAuthenticationContext<TwitchIdentity.Client>>
{
    protected override string Path => "/authorization/users";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("user_id", UserIds.Select(x => x.Value));
    public TwitchRequestAuthenticationContext<TwitchIdentity.Client> AuthenticationContext
    {
        get => field ?? TwitchRequestAuthenticationContext.Default;
        init;
    }

    /// <summary>
    /// The user id(s) of the user(s) you want to check authorization for.
    /// </summary>
    /// <remarks>
    /// A maximum of 10 user ids can be specified.
    /// </remarks>
    public required IEnumerable<UserId> UserIds { get; init; }
}
