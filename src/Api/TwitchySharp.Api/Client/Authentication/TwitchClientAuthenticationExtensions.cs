using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

public static class TwitchClientAuthenticationExtensions
{
    /// <summary>
    /// Configure the client to use app access tokens for user authenticated endpoints that support prior authorization.
    /// </summary>
    /// <param name="client">The client to configure.</param>
    /// <returns>A new <see cref="TwitchClient"/> configured to enable prior authorization.</returns>
    public static TwitchClient AlwaysUsePriorAuthorization(this TwitchClient client)
        => client
            .When((scope, ct) => scope.ResolveOrDefault<ITwitchRequestAuthenticationContext<TwitchIdentity>>(ct).MapAsync(context => context is ISupportPriorAuthorization))
            .SetFixed<RequestDependencyConditionalConfiguration<TwitchClient>, BearerTokenType?>(BearerTokenType.AppAccessToken)
            .EndWhen();

    /// <summary>
    /// Configure the <see cref="TwitchClient"/> to use a fixed <see cref="ClientId"/>.
    /// </summary>
    /// <remarks>
    /// The previous <see cref="TwitchIdentity"/> configuration will be evaluated before this one,
    /// with this configuration only applying if the previous <see cref="TwitchIdentity"/> has a <see langword="null"/> <see cref="ClientId"/>.
    /// </remarks>
    /// <param name="client">The client to configure.</param>
    /// <param name="defaultClientId">The <see cref="ClientId"/> to use for all requests.</param>
    /// <returns>A new <see cref="TwitchClient"/> configured to the use <paramref name="defaultClientId"/> for all requests.</returns>
    public static TwitchClient WithDefaultClientId(
        this TwitchClient client,
        ClientId defaultClientId
        )
        => client.Configure<TwitchClient, TwitchIdentity?>(next => (context, ct) =>
            next(context, ct).MapAsync(identity => identity is { ClientId: null } overrideClientId
                ? overrideClientId with { ClientId = defaultClientId }
                : identity));
}
