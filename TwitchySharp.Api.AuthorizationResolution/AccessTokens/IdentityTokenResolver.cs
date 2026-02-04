using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves an <see cref="AccessToken"/> by pattern matching on the <see cref="IRequireAuthorization.Identity"/>
/// and dispatching to the appropriate identity-specific resolver.
/// </summary>
/// <param name="UserAccessTokenResolver">Resolver for <see cref="UserIdentity"/> requests.</param>
/// <param name="AppAccessTokenResolver">Resolver for requests using <see cref="ClientIdentity"/> or <see cref="TwitchApiIdentity"/> where <see cref="TwitchApiIdentity.ClientId"/> is not <see langword="null"/>.</param>
/// <param name="ExtensionJwtResolver">Resolver for <see cref="ExtensionIdentity"/> requests.</param>
public record IdentityTokenResolver(
    IResolveUserAccessToken? UserAccessTokenResolver = null,
    IResolveAppAccessToken? AppAccessTokenResolver = null,
    IResolveExtensionJsonWebToken? ExtensionJwtResolver = null
) : ITokenResolver
{
    /// <inheritdoc/>
    public async ValueTask<AccessToken?> GetToken(ITwitchRequest request, CancellationToken ct = default)
    {
        if (request is not IRequireAuthorization authRequest)
            return null;

        return authRequest.Identity switch
        {
            UserIdentity user when UserAccessTokenResolver is not null
                => ExtractToken(await UserAccessTokenResolver.GetToken(new UserAccessTokenKey { User = user, ValidScopes = authRequest.ValidScopes }, ct)),
            ClientIdentity client when AppAccessTokenResolver is not null
                => await AppAccessTokenResolver.GetToken(client, ct),
            ExtensionIdentity extension when ExtensionJwtResolver is not null
                => await ExtensionJwtResolver.GetToken(extension, ct),
            TwitchApiIdentity { ClientId: not null } identity when AppAccessTokenResolver is not null
                => await AppAccessTokenResolver.GetToken(new ClientIdentity(identity.ClientId.Value), ct),
            _ => null
        };
    }

    private static AccessToken? ExtractToken(UserAccessTokenResolutionResult? result)
    {
        return result switch
        {
            UserAccessTokenResolutionResult.Success success => success.Token,
            UserAccessTokenResolutionResult.Expired expired => expired.Token,
            _ => null
        };
    }
}
