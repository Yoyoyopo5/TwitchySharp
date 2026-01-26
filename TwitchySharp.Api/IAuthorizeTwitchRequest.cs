using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api;

/// <summary>
/// Determines the authorization headers to use for a Twitch API request.
/// </summary>
/// <remarks>
/// This interface receives the full <see cref="ITwitchRequest"/> to allow custom implementations
/// to make authorization decisions based on endpoint-specific context (e.g., different tokens
/// for different endpoints, or request parameters).
/// <br/>
/// Use <see cref="DefaultRequestAuthorizer"/> for standard authorization scenarios.
/// </remarks>
public interface IAuthorizeTwitchRequest
{
    /// <summary>
    /// Gets the authorization options for the given request.
    /// </summary>
    /// <param name="request">The full request that needs authorization.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>
    /// The authorization options containing the ClientId and BearerToken to set as headers,
    /// or <see langword="null"/> if the request does not require authorization.
    /// </returns>
    ValueTask<TwitchAuthorizationRequestOptions?> GetAuthorization(ITwitchRequest request, CancellationToken ct = default);
}

/// <summary>
/// The default <see cref="IAuthorizeTwitchRequest"/> implementation that resolves authorization
/// using an <see cref="IResolveClientIdentity"/> for client ID resolution and an
/// <see cref="ITokenResolver"/> for access token resolution.
/// </summary>
/// <param name="clientIdentityResolver">
/// The resolver for determining which client identity to use.
/// Use <see cref="SingleClientIdentityResolver"/> for simple single-client scenarios.
/// </param>
/// <param name="tokenResolver">The resolver for obtaining access tokens based on identity and required scopes.</param>
public class DefaultRequestAuthorizer(IResolveClientIdentity clientIdentityResolver, ITokenResolver tokenResolver)
    : IAuthorizeTwitchRequest
{
    private readonly IResolveClientIdentity _clientIdentityResolver = clientIdentityResolver;
    private readonly ITokenResolver _tokenResolver = tokenResolver;

    /// <inheritdoc/>
    public async ValueTask<TwitchAuthorizationRequestOptions?> GetAuthorization(ITwitchRequest request, CancellationToken ct = default)
    {
        if (request is not IRequireAuthorization auth)
            return null;

        // Resolve the client identity fallback and apply it to the request's identity
        ClientIdentity? fallbackClient = await _clientIdentityResolver.GetClientId(request, ct).ConfigureAwait(false);
        TwitchApiIdentity resolvedIdentity = auth.Identity.WithFallbackClient(fallbackClient);

        // Resolve the access token, respecting any override on the request
        AccessToken? token = auth.OverrideAccessToken
            ?? await _tokenResolver.GetToken(resolvedIdentity, auth.ValidScopes, ct).ConfigureAwait(false);

        return new TwitchAuthorizationRequestOptions(resolvedIdentity.ClientId, token);
    }
}
