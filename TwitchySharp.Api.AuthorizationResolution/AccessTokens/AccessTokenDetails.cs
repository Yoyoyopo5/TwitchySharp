namespace TwitchySharp.Api.AuthorizationResolution;
/// <summary>
/// Generic base abstract record for access token details.
/// </summary>
/// <remarks>
/// See <see cref="UserAccessTokenDetails"/>, <see cref="AppAccessTokenDetails"/>, and <see cref="ExtensionJsonWebToken"/>.
/// </remarks>
public abstract record AccessTokenDetails<TIdentity, TToken> : IAccessTokenDetails
    where TIdentity : TwitchApiIdentity
    where TToken : AccessToken
{
    /// <inheritdoc cref="IAccessTokenDetails.Identity"/>
    public abstract TIdentity Identity { get; init; }
    /// <inheritdoc cref="IAccessTokenDetails.AccessToken"/>
    public abstract TToken AccessToken { get; init; }
    public DateTimeOffset ExpiresAt { get; init; }
    TwitchApiIdentity IAccessTokenDetails.Identity => Identity;
    AccessToken IAccessTokenDetails.AccessToken => AccessToken;
}