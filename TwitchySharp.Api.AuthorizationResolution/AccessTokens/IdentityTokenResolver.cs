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
/// If the passed identity is <see cref="TwitchApiIdentity.Default"/>, an <see cref="AccessTokenResolutionResult.Unavailable"/> is returned.
/// </remarks>
/// <param name="UserAccessTokenResolver">Resolver for <see cref="UserIdentity"/> requests.</param>
/// <param name="AppAccessTokenResolver">Resolver for requests using <see cref="ClientIdentity"/> or <see cref="TwitchApiIdentity"/> where <see cref="TwitchApiIdentity.ClientId"/> is not <see langword="null"/>.</param>
/// <param name="ExtensionJwtResolver">Resolver for <see cref="ExtensionIdentity"/> requests.</param>
public record IdentityTokenResolver(
    IResolveAccessToken<UserAccessTokenKey>? UserAccessTokenResolver = null,
    IResolveAccessToken<ClientIdentity>? AppAccessTokenResolver = null,
    IResolveAccessToken<ExtensionIdentity>? ExtensionJwtResolver = null
    ) : IResolveAccessToken<IRequireAuthorization>
{
    /// <inheritdoc/>
    /// <exception cref="NotSupportedException">
    /// Unsupported derived <see cref="TwitchApiIdentity"/> type.
    /// </exception>
    public async ValueTask<AccessTokenResolutionResult> GetToken(IRequireAuthorization hasIdentity, CancellationToken ct = default)
    {
        return hasIdentity.Identity switch
        {
            UserIdentity user when UserAccessTokenResolver is not null
                => await UserAccessTokenResolver.GetToken(new UserAccessTokenKey { User = user, ValidScopes = hasIdentity.ValidScopes }, ct),
            ClientIdentity client when AppAccessTokenResolver is not null
                => await AppAccessTokenResolver.GetToken(client, ct),
            ExtensionIdentity extension when ExtensionJwtResolver is not null
                => await ExtensionJwtResolver.GetToken(extension, ct),
            TwitchApiIdentity { ClientId: not null } identity when AppAccessTokenResolver is not null
                => await AppAccessTokenResolver.GetToken(new ClientIdentity(identity.ClientId.Value), ct), 
            TwitchApiIdentity
                => new AccessTokenResolutionResult.Unavailable(),
            _ => throw new NotSupportedException($"Unsupported identity type {hasIdentity.Identity.GetType().Name} when resolving access token by identity.")
        };
    }
}
