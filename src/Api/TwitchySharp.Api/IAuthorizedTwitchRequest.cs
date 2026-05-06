using System.Collections.Generic;
using System.Collections.Immutable;

namespace TwitchySharp.Api;
/// <summary>
/// A Twitch request that requires the ClientId and Authorization headers to be set.
/// </summary>
public interface IAuthorizedTwitchRequest
{
    /// <summary>
    /// The authorization context to use for the request.
    /// </summary>
    TwitchRequestAuthorizationContext AuthorizationContext { get; }
}

/// <summary>
/// The authorization context a Twitch request is to be made under.
/// </summary>
/// <remarks>
/// Should be used to set the correct Twitch authorization headers (Client-Id and Authorization) for the request.
/// </remarks>
public readonly record struct TwitchRequestAuthorizationContext()
{
    /// <summary>
    /// The identity to use for the request.
    /// </summary>
    public required TwitchIdentity Identity { get; init; }
    /// <summary>
    /// One of these user scopes is required.
    /// </summary>
    public IReadOnlySet<Scope> ValidScopes { get; init; } = ImmutableHashSet<Scope>.Empty;
    /// <summary>
    /// Allows for manually setting an access token.
    /// </summary>
    /// <remarks>
    /// This property should override all other access token configuration
    /// and be guaranteed to be the bearer authorization used for the request.
    /// </remarks>
    public IAccessToken? AccessToken { get; init; }
}
