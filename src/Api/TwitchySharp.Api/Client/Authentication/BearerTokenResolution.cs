using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

public static class BearerTokenResolution
{
    /// <summary>
    /// Configure the client to use app access tokens for user authenticated endpoints that support prior authorization.
    /// </summary>
    /// <param name="client">The client to configure.</param>
    /// <returns>A new <see cref="TwitchClient"/> configured to enable prior authorization.</returns>
    public static TwitchClient AlwaysUsePriorAuthorization(this TwitchClient client)
        => client
            .When((scope, ct) => scope.ResolveOrDefault<ITwitchRequestAuthenticationContext<TwitchIdentity>>(ct).MapAsync(context => context is ISupportPriorAuthorization))
            .SetFixed(BearerTokenType.AppAccessToken)
            .EndWhen();
}
