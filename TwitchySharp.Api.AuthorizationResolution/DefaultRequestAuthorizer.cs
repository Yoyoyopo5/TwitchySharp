using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Combines the <see cref="DefaultClientIdentityResolver"/> and <see cref="DefaultTokenResolver>"/> to determine full Twitch authorization header values.
/// </summary>
/// <param name="client">The <see cref="ClientIdentity"/> that requests will fall back to if <see cref="TwitchApiIdentity.ClientId"/> is <see langword="null"/>.</param>
/// <param name="identityResolver">The identity resolver to use.</param>
public class DefaultRequestAuthorizer(ClientIdentity client, IdentityTokenResolver identityResolver)
    : IAuthorizeTwitchRequest
{
    // Eventually I want to get rid of this class and absorb its functionality into a delegating handler.

    private readonly DefaultClientIdentityResolver _clientResolver = new(client);
    private readonly DefaultTokenResolver _tokenResolver = new(identityResolver);

    public async ValueTask<TwitchAuthorizationRequestOptions?> GetAuthorization(ITwitchRequest request, CancellationToken ct = default)
    {
        if (request is not IRequireAuthorization requiresAuth)
            return null;
        
        ClientId? clientIdentity = (await _clientResolver.GetClientId(request, ct))?.ClientId;
        IRequireAuthorization configuredAuthoirization = clientIdentity.HasValue
            ? AuthorizationRequirement.ConfigureIdentity(requiresAuth, clientIdentity.Value)
            : requiresAuth;
        AccessToken? accessToken = (await _tokenResolver.GetToken(configuredAuthoirization, ct) as IHaveAccessToken<AccessToken>)?.AccessToken;
        return new TwitchAuthorizationRequestOptions(clientIdentity, accessToken);
    }

    /// <summary>
    /// Used to fallback to a configured <see cref="ClientId"/> when one is not provided by the request.
    /// </summary>
    private record AuthorizationRequirement
        : IRequireAuthorization
    {
        public static AuthorizationRequirement ConfigureIdentity(IRequireAuthorization requiresAuth, ClientId fallbackClientId)
            => new()
            {
                Identity = requiresAuth.Identity.ClientId is not null
                    ? requiresAuth.Identity
                    : requiresAuth.Identity with { ClientId = fallbackClientId },
                ValidScopes = requiresAuth.ValidScopes,
                OverrideAccessToken = requiresAuth.OverrideAccessToken
            };

        public TwitchApiIdentity Identity { get; init; } = TwitchApiIdentity.Default;
        public IReadOnlySet<Scope> ValidScopes { get; init; } = ImmutableHashSet<Scope>.Empty;
        public AccessToken? OverrideAccessToken { get; init; }
    }
}
