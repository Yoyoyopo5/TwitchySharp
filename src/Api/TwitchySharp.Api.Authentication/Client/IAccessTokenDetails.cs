namespace TwitchySharp.Api.Authentication;
/// <summary>
/// Contains information about a specific access token and the context it belongs to.
/// </summary>
public interface IAccessTokenDetails<out TIdentity>
    where TIdentity : TwitchIdentity
{
    /// <summary>
    /// The identity associated with the access token.
    /// </summary>
    TIdentity Identity { get; }
    /// <summary>
    /// The bearer token.
    /// </summary>
    BearerToken BearerToken { get; }
    /// <summary>
    /// The date and time when the token expires.
    /// </summary>
    DateTimeOffset ExpiresAt { get; }
}
