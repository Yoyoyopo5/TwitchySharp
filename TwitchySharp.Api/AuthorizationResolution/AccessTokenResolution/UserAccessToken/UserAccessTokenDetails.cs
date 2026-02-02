using System.Collections.Generic;
using System.Collections.Immutable;

namespace TwitchySharp.Api;
/// <summary>
/// Contains information about a specific <see cref="UserAccessToken"/>,
/// including its associated user identity, expiration, refresh token, and scopes. 
/// </summary>
public record UserAccessTokenDetails
    : AccessTokenDetails
{
    /// <summary>
    /// The user and client pair that this access token is associated with.
    /// </summary>
    public required UserIdentity User { get; init; }
    /// <summary>
    /// The access token.
    /// </summary>
    public required UserAccessToken AccessToken { get; init; }
    /// <summary>
    /// The refresh token that can be used to obtain a new access token when this one expires, if any.
    /// </summary>
    public RefreshToken? RefreshToken { get; init; }
    /// <summary>
    /// The scopes present on this access token.
    /// </summary>
    public IReadOnlySet<Scope> Scopes { get; init; } = ImmutableHashSet<Scope>.Empty;
}
