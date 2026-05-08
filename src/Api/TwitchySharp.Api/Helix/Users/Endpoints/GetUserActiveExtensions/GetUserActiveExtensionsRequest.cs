using System.Collections.Immutable;
using System.Net.Http;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Gets the channel's extension slots, showing any currently active extensions.
/// </summary>
/// <remarks>
/// <para>
/// Requires an app or user access token.
/// To include extensions that are under development, use <see cref="IncludingUnderDevelopment"/>
/// which requires a user access token with <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.UserEditBroadcast"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-active-extensions">Get User Active Extensions</see> for more information.
/// </remarks>
public record GetUserActiveExtensionsRequest
    : TwitchHelixRequest<GetUserActiveExtensionsResponse>
{
    protected override string Path => "/users/extensions";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = UserIdentity ?? TwitchIdentity.Client.Default,
        ValidScopes = ImmutableHashSet.Create(Scope.UserReadBroadcast, Scope.UserEditBroadcast)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("user_id", UserId);

    /// <summary>
    /// The user id of the broadcaster to get active extensions for.
    /// </summary>
    public required UserId UserId { get; init; }

    /// <summary>
    /// Returns a new request configured to include extensions that are under development.
    /// </summary>
    /// <remarks>
    /// This sets the identity to require a user access token for the specified <see cref="UserId"/>.
    /// The token must include <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.UserEditBroadcast"/>.
    /// </remarks>
    /// <returns>A new <see cref="GetUserActiveExtensionsRequest"/> with user identity set.</returns>
    public GetUserActiveExtensionsRequest IncludingUnderDevelopment()
        => this with { UserIdentity = new TwitchIdentity.User(UserId) };

    private TwitchIdentity.User? UserIdentity { get; init; }
}
