using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Users;
/// <summary>
/// Gets a list of all extensions (both active and inactive) that a broadcaster has installed.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadBroadcast"/> or <see cref="Scope.UserEditBroadcast"/>.
/// <see cref="Scope.UserEditBroadcast"/> is required to get inactive extensions.
/// The user who created the token is the broadcaster to get extensions for.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-user-extensions">Get User Extensions</see> for more information.
/// </remarks>
public record GetUserExtensionsRequest
    : TwitchHelixRequest<GetUserExtensionsResponse>
{
    protected override string Path => "/users/extensions/list";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(UserId),
        ValidScopes = ImmutableHashSet.Create(Scope.UserReadBroadcast, Scope.UserEditBroadcast)
    };

    /// <summary>
    /// The id of the user to get extensions for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId UserId { get; init; }
}
