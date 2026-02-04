using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// The default <see cref="IAuthorizeTwitchRequest"/> implementation.
/// </summary>
/// <remarks>
/// <para>
/// Resolves authorization headers using an <see cref="IResolveClientIdentity"/> for client id resolution 
/// and an <see cref="IResolveAccessToken"/> for access token (bearer auth) resolution.
/// </para>
/// <para>
/// Resolves <see langword="null"/> if the passed <see cref="ITwitchRequest"/> does not implement <see cref="IRequireAuthorization"/>.
/// </para>
/// </remarks>
/// <param name="clientIdentityResolver">
/// The resolver for determining which client identity to use.
/// Use <see cref="SingleClientIdentityResolver"/> for simple single-client scenarios.
/// </param>
/// <param name="tokenResolver">The resolver for obtaining access tokens based on identity and required scopes.</param>
public class DefaultRequestAuthorizer(IResolveClientIdentity clientIdentityResolver, IResolveAccessToken tokenResolver)
    : IAuthorizeTwitchRequest
{
    private readonly IResolveClientIdentity _clientIdentityResolver = clientIdentityResolver;
    private readonly IResolveAccessToken _tokenResolver = tokenResolver;

    public async ValueTask<TwitchAuthorizationRequestOptions?> GetAuthorization(ITwitchRequest request, CancellationToken ct = default)
    {
        if (request is not IRequireAuthorization) // Does not require authorization.
            return null;

        ClientIdentity? identity = await ResolveClientIdentity(request, ct).ConfigureAwait(false);
        // When we migrate to twitch delegating handlers, we will need to split this class up into a pipeline,
        // So that the token resolver can have access to the resolved client identity as it is often null in requests.
        AccessToken? token = await ResolveAccessToken(request, ct).ConfigureAwait(false);

        return new TwitchAuthorizationRequestOptions(identity?.ClientId, token);
    }

    private ValueTask<ClientIdentity?> ResolveClientIdentity(ITwitchRequest request, CancellationToken ct)
        => _clientIdentityResolver.GetClientId(request, ct);

    private ValueTask<AccessToken?> ResolveAccessToken(ITwitchRequest request, CancellationToken ct)
        => _tokenResolver.GetToken(request, ct);
}
