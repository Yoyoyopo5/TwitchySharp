using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Determines the authorization headers to use for a Twitch API request.
/// </summary>
/// <remarks>
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
/// The default <see cref="IAuthorizeTwitchRequest"/> implementation.
/// </summary>
/// <remarks>
/// Resolves authorization headers using an <see cref="IResolveClientIdentity"/> for client id resolution 
/// and an <see cref="ITokenResolver"/> for access token (bearer auth) resolution.
/// </remarks>
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
        TwitchApiIdentity? identity = await ResolveIdentity(request, ct);
        AccessToken? token = await ResolveAccessToken(request, ct);

        return new TwitchAuthorizationRequestOptions(identity?.ClientId, token);
    }

    private async ValueTask<TwitchApiIdentity?> ResolveIdentity(ITwitchRequest request, CancellationToken ct)
        => request switch
        {
            IRequireAuthorization hasIdentity => hasIdentity.Identity.WithFallbackClient(await _clientIdentityResolver.GetClientId(request, ct).ConfigureAwait(false)),
            _ => await _clientIdentityResolver.GetClientId(request, ct).ConfigureAwait(false)
            // Potential problem here:
            // What if the caller explicitly wants to not set an Identity?
            // There exists a TwitchApiIdentity.None, however it doesn't do anything special
            // and just keeps the ClientId? property null, which would be overwritten here.
            // We need a way to explicitly signal to DefaultRequestAuthorizer not to set the fallback client identity.
        };

    private async ValueTask<AccessToken?> ResolveAccessToken(ITwitchRequest request, CancellationToken ct)
        => request switch
        {
            IRequireAuthorization hasIdentity => hasIdentity.OverrideAccessToken ?? await _tokenResolver.GetToken(request, ct).ConfigureAwait(false),
            _ => await _tokenResolver.GetToken(request, ct).ConfigureAwait(false)
        };
}
