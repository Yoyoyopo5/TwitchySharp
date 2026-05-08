namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves a <see cref="ClientId"/> for a request requiring authorization.
/// </summary>
/// <param name="context">The authorization requirement.</param>
/// <returns>A <see cref="ValueTask"/> containing the resolved <see cref="ClientId"/>, if any.</returns>
public delegate ValueTask<ClientId?> ClientIdResolver(TwitchRequestAuthorizationContext context, CancellationToken ct = default);

internal static class ClientIdentityResolution
{
    private readonly static ClientIdResolver GetConfiguredClientId =
        (context, ct) => ValueTask.FromResult(context.Identity.ClientId);

    /// <returns>
    /// A resolver function returning the <see cref="TwitchIdentity.ClientId"/> from the <see cref="TwitchRequestAuthorizationContext.Identity"/>, if it is not <see langword="null"/>.
    /// Otherwise, the output of the <paramref name="next"/>.
    /// </returns>
    public static ClientIdResolver UseConfiguredClientId(ClientIdResolver next)
        => async (context, ct) => (await GetConfiguredClientId(context, ct)) ?? await next(context, ct);

    /// <returns>
    /// A function returning <inheritdoc cref="UseConfiguredClientId(ClientIdResolver)"/>
    /// </returns>
    public static Func<ClientIdResolver, ClientIdResolver> UseConfiguredClientId()
        => next => async (context, ct) => (await GetConfiguredClientId(context, ct)) ?? await next(context, ct);

    /// <returns>
    /// A <see cref="ClientIdResolver"/> that always returns the <paramref name="clientId"/>.
    /// </returns>
    public static ClientIdResolver UseFallbackClientId(ClientId? clientId)
        => (_, _) => ValueTask.FromResult(clientId);

    /// <summary>
    /// Short-circuits to <see langword="null"/> <see cref="ClientId"/> when the explicit
    /// <see cref="TwitchIdentity.None"/> is used in the <see cref="TwitchRequestAuthorizationContext"/>.
    /// </summary>
    public static ClientIdResolver UseNoneIdentity(ClientIdResolver next)
        => (context, ct)
            => context.Identity is TwitchIdentity.None
            ? ValueTask.FromResult<ClientId?>(null)
            : next(context, ct);
}
