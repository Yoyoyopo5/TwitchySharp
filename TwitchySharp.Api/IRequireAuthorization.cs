using System.Collections.Generic;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api;
/// <summary>
/// A Twitch request that requires the ClientId and Authorization headers to be set.
/// </summary>
public interface IRequireAuthorization
{
    /// <summary>
    /// The identity to use for the request.
    /// </summary>
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
    /// <summary>
    /// Returns a copy of this request with the <see cref="Identity"/> modified to use
    /// the specified <paramref name="client"/> as a fallback if <see cref="TwitchApiIdentity.ClientId"/> is not set.
    /// </summary>
    /// <remarks>
    /// This method preserves the full request type and all its properties, unlike creating
    /// a wrapper object which would lose endpoint-specific context.
    /// Implementations using C# records can simply use: <c>this with { Identity = Identity.WithFallbackClient(client) }</c>
    /// </remarks>
    /// <param name="client">The fallback client identity to use if <see cref="TwitchApiIdentity.ClientId"/> is null.</param>
    /// <returns>A copy of the request with the identity potentially updated.</returns>
    public IRequireAuthorization WithClientFallback(ClientIdentity? client);
}
