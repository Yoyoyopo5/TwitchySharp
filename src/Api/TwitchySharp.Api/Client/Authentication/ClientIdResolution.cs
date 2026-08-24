namespace TwitchySharp.Api;

public static class ClientIdResolution
{
    /// <summary>
    /// Configure the <see cref="TwitchClient"/> to use a fixed <see cref="ClientId"/>.
    /// </summary>
    /// <remarks>
    /// The previous <see cref="ClientId"/> configuration will be evaluated before this one,
    /// with this configuration only applying if the previous returned a <see langword="null"/> <see cref="ClientId"/>.
    /// </remarks>
    /// <param name="client">The client to configure.</param>
    /// <param name="fixedClientId">The <see cref="ClientId"/> to use for all requests.</param>
    /// <returns>A new <see cref="TwitchClient"/> configured to the use <paramref name="fixedClientId"/> for all requests.</returns>
    public static TwitchClient WithClientId(
        this TwitchClient client,
        ClientId fixedClientId
        )
        => client.ConfigureAsNullCoalesce((context, _) => ValueTask.FromResult(new DependencyResult<ClientId?>(fixedClientId, context)));

    /// <summary>
    /// Configure the <see cref="TwitchClient"/> to use the <see cref="ClientId"/> from the
    /// <see cref="ITwitchRequestAuthenticationContext{TIdentity}.Identity"/> of each request.
    /// </summary>
    /// <remarks>
    /// <inheritdoc cref="WithClientId(TwitchClient, ClientId)"/>
    /// </remarks>
    /// <param name="client">The client to configure.</param>
    /// <returns>A new <see cref="TwitchClient"/> configured to use the <see cref="ClientId"/> from each authenticated request's <see cref="ITwitchRequestAuthenticationContext{TIdentity}"/>.</returns>
    public static TwitchClient UseClientIdFromRequestAuthenticationContext(
        this TwitchClient client
        )
        => client.ConfigureAsNullCoalesce((context, _) =>
            ValueTask.FromResult(new DependencyResult<ClientId?>(context.Request is IAuthenticatedTwitchRequest authenticatedRequest
                ? authenticatedRequest.AuthenticationContext.Identity.ClientId
                : null,
                context
                )));
}
