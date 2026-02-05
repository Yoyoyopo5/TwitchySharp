namespace TwitchySharp.Api.AuthorizationResolution;
/// <summary>
/// Interface for access token detail types.
/// </summary>
/// <remarks>
/// See <see cref="UserAccessTokenDetails"/>, <see cref="AppAccessTokenDetails"/>, and <see cref="ExtensionJsonWebToken"/>.
/// </remarks>
public interface IAccessTokenDetails
{
    /// <summary>
    /// The identity associated with the access token.
    /// </summary>
    TwitchApiIdentity Identity { get; }
    /// <summary>
    /// The access token.
    /// </summary>
    AccessToken AccessToken { get; }
    /// <summary>
    /// The date and time the access token expires.
    /// </summary>
    DateTimeOffset ExpiresAt { get; }
}