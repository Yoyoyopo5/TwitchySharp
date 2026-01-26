using System.Collections.Generic;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api;
/// <summary>
/// A Twitch request that requires the ClientId and Authorization headers to be set.
/// </summary>
/// <remarks>
/// Requests implementing this interface will be processed by <see cref="IAuthorizeTwitchRequest"/>
/// to resolve the appropriate authorization headers before being sent.
/// </remarks>
public interface IRequireAuthorization
{
    /// <summary>
    /// The identity to use for the request.
    /// </summary>
    /// <remarks>
    /// This identity determines the context for token resolution. The <see cref="TwitchApiIdentity.ClientId"/>
    /// may be null if the request relies on a fallback client ID from <see cref="IResolveClientIdentity"/>.
    /// </remarks>
    public TwitchApiIdentity Identity { get; }
    /// <summary>
    /// One of these user scopes is required.
    /// </summary>
    public IEnumerable<Scope> ValidScopes { get; }
    /// <summary>
    /// Allows for manually setting an access token.
    /// </summary>
    /// <remarks>
    /// This property should override all other access token configuration
    /// and be guaranteed to be the bearer authorization used for the request.
    /// </remarks>
    public AccessToken? OverrideAccessToken { get; }
}
