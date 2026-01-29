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
/// <para>
/// Resolves authorization headers using an <see cref="IResolveClientIdentity"/> for client id resolution 
/// and an <see cref="ITokenResolver"/> for access token (bearer auth) resolution.
/// </para>
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the <paramref name="request"/> implements <see cref="IRequireAuthorization"/>, its <see cref="IRequireAuthorization.Identity"/>
    /// and <see cref="IRequireAuthorization.OverrideAccessToken"/> will be preferentially used to set the authorization headers.
    /// </para>
    /// </remarks>
    /// <returns>
    /// <inheritdoc/>
    /// </returns>
    public async ValueTask<TwitchAuthorizationRequestOptions?> GetAuthorization(ITwitchRequest request, CancellationToken ct = default)
    {
        TwitchApiIdentity? identity = await ResolveIdentity(request, ct).ConfigureAwait(false);
        AccessToken? token = await ResolveAccessToken(request, ct).ConfigureAwait(false);

        return new TwitchAuthorizationRequestOptions(identity?.ClientId, token);
    }

    private async ValueTask<TwitchApiIdentity?> ResolveIdentity(ITwitchRequest request, CancellationToken ct)
        => request switch
        {
            IRequireAuthorization hasIdentity => hasIdentity.Identity.WithFallbackClient(await _clientIdentityResolver.GetClientId(request, ct).ConfigureAwait(false)),
            _ => await _clientIdentityResolver.GetClientId(request, ct).ConfigureAwait(false)
        };

    private async ValueTask<AccessToken?> ResolveAccessToken(ITwitchRequest request, CancellationToken ct)
        => request switch
        {
            IRequireAuthorization hasIdentity => hasIdentity.OverrideAccessToken ?? await _tokenResolver.GetToken(request, ct).ConfigureAwait(false),
            _ => await _tokenResolver.GetToken(request, ct).ConfigureAwait(false)
        };
}
