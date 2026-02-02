using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api;
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
}
