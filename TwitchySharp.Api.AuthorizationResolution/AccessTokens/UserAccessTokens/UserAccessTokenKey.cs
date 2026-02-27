using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;
/// <summary>
/// A key used to retrieve a specific <see cref="UserAccessTokenDetails"/>.
/// </summary>
internal record UserAccessTokenKey
    : AccessTokenKey<UserIdentity>
{
    /// <summary>
    /// The user identity.
    /// </summary>
    public required new UserIdentity Identity { get; init; }
    /// <summary>
    /// The access token must have one of these scopes.
    /// </summary>
    public IReadOnlySet<Scope> ValidScopes { get; init; } = ImmutableHashSet<Scope>.Empty;

    /// <summary>
    /// Compares equality based on user identity and valid scopes.
    /// </summary>
    public virtual bool Equals(UserAccessTokenKey? other) =>
          other is not null &&
          Identity == other.Identity &&
          ValidScopes.SetEquals(other.ValidScopes);

    /// <summary>
    /// Gets a hash code for the user identity and scope combination.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Identity);
        // Order-independent hash for set
        int scopeHash = 0;
        foreach (Scope scope in ValidScopes)
            scopeHash ^= scope.GetHashCode();
        hash.Add(scopeHash);
        return hash.ToHashCode();
    }
}
