using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves a <see cref="ClientId"/> for a request requiring authorization.
/// </summary>
/// <param name="request">The authorization requirement.</param>
/// <returns>A <see cref="ValueTask"/> containing the resolved <see cref="ClientId"/>, if any.</returns>
public delegate ValueTask<ClientId?> ClientIdResolver(IRequireAuthorization request, CancellationToken ct = default);

internal static class ClientIdentityResolution
{
    private readonly static ClientIdResolver GetConfiguredClientId =
        (request, ct) => ValueTask.FromResult(request.Identity.ClientId);

    /// <returns>
    /// The <see cref="TwitchApiIdentity.ClientId"/> from the <see cref="IRequireAuthorization.Identity"/>, if it is not <see langword="null"/>.
    /// Otherwise, the output of the <paramref name="next"/>.
    /// </returns>
    public static ClientIdResolver UseConfiguredClientId(ClientIdResolver next)
        => async (request, ct) => (await GetConfiguredClientId(request, ct)) ?? await next(request, ct);

    public static Func<ClientIdResolver, ClientIdResolver> UseConfiguredClientId()
        => next => async (request, ct) => (await GetConfiguredClientId(request, ct)) ?? await next(request, ct);

    /// <returns>
    /// A <see cref="ClientIdResolver"/> that always returns the <paramref name="clientId"/>.
    /// </returns>
    public static ClientIdResolver UseFallbackClientId(ClientId? clientId)
        => (_, _) => ValueTask.FromResult(clientId);
}
