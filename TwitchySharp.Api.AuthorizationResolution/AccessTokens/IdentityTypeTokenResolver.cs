using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves an <see cref="AccessToken"/> by pattern matching on the <see cref="IRequireAuthorization.Identity"/>
/// and dispatching to the appropriate identity-specific resolver.
/// </summary>
/// <remarks>
/// If the passed identity is <see cref="TwitchApiIdentity.Default"/>, an <see cref="AccessTokenDetailsResolutionResult.Unavailable"/> is returned.
/// </remarks>
/// <param name="UserAccessTokenResolver">Resolver for <see cref="UserIdentity"/> requests.</param>
/// <param name="AppAccessTokenResolver">Resolver for requests using <see cref="ClientIdentity"/> or <see cref="TwitchApiIdentity"/> where <see cref="TwitchApiIdentity.ClientId"/> is not <see langword="null"/>.</param>
/// <param name="ExtensionJwtResolver">Resolver for <see cref="ExtensionIdentity"/> requests.</param>
public record IdentityTypeTokenResolver(
    Func<UserAccessTokenKey, AccessTokenDetailsResolutionResult>? UserAccessTokenResolver = null,
    Func<AccessTokenKey, AccessTokenDetailsResolutionResult>? AppAccessTokenResolver = null,
    Func<AccessTokenKey, AccessTokenDetailsResolutionResult>? ExtensionJwtResolver = null
    ) : IResolveAccessToken<IRequireAuthorization>
{
    /// <summary>
    /// Resolves an <see cref="AccessToken"/> based on a provided <see cref="IRequireAuthorization"/>.
    /// </summary>
    /// <param name="hasIdentity">The object requiring authorization to resolve an <see cref="AccessToken"/> for.</param>
    /// <returns>A <see cref="ValueTask"/> containing a <see cref="AccessTokenDetailsResolutionResult"/> resolved from the provided <see cref="IRequireAuthorization"/>.</returns>
    /// <exception cref="NotSupportedException"></exception>
    public ValueTask<AccessTokenDetailsResolutionResult> ResolveAsync(IRequireAuthorization hasIdentity, CancellationToken ct = default)
        => ResolveAsync(hasIdentity.Identity switch
        {
            UserIdentity user 
                => new UserAccessTokenKey { User = user, ValidScopes = hasIdentity.ValidScopes },
            ClientIdentity client 
                => new AccessTokenKey { Identity = client },
            ExtensionIdentity extension 
                => new AccessTokenKey { Identity = extension },
            TwitchApiIdentity { ClientId: not null } identity 
                => new AccessTokenKey { Identity = new ClientIdentity(identity.ClientId.Value) },
            TwitchApiIdentity identity 
                => new AccessTokenKey { Identity = identity },
            _ => throw new NotSupportedException($"Unsupported identity type {hasIdentity.Identity.GetType().Name} when resolving access token by identity.")
        }, ct);

    private async ValueTask<AccessTokenDetailsResolutionResult> ResolveAsync(AccessTokenKey key, CancellationToken ct = default)
    {
        return key switch
        {
            UserAccessTokenKey user when UserAccessTokenResolver is not null
                => await UserAccessTokenResolver.ResolveAsync(user, ct),
            AccessTokenKey<ClientIdentity> client when AppAccessTokenResolver is not null
                => await AppAccessTokenResolver.ResolveAsync(client, ct),
            AccessTokenKey<ExtensionIdentity> extension when ExtensionJwtResolver is not null
                => await ExtensionJwtResolver.ResolveAsync(extension, ct),
            AccessTokenKey<TwitchApiIdentity>
                => new AccessTokenDetailsResolutionResult.Unavailable(),
            _ => throw new NotSupportedException($"Unsupported key type {key.GetType().Name} when resolving access token by identity.")
        };
    }
}
