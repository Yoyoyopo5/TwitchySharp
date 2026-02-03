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
public record UserAccessTokenKey
{
    /// <summary>
    /// The access token must have been created for this user and client combination.
    /// </summary>
    public required UserIdentity User { get; init; }
    /// <summary>
    /// The access token must have one of these scopes.
    /// </summary>
    public IReadOnlySet<Scope> ValidScopes { get; init; } = ImmutableHashSet<Scope>.Empty;

    public virtual bool Equals(UserAccessTokenKey? other) =>
          other is not null &&
          User == other.User &&
          ValidScopes.SetEquals(other.ValidScopes);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(User);
        // Order-independent hash for set
        int scopeHash = 0;
        foreach (Scope scope in ValidScopes)
            scopeHash ^= scope.GetHashCode();
        hash.Add(scopeHash);
        return hash.ToHashCode();
    }
}
