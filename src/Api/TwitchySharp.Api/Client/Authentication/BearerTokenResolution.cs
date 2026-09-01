using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

public static class BearerTokenResolution
{
    /// <summary>
    /// Set a <see cref="BearerToken"/> resolver for a request with a non-null <see cref="ITwitchRequestAuthenticationContext{T}"/>
    /// when <see cref="ITwitchRequestAuthenticationContext{TIdentity}.TokenType"/> is a specified value.
    /// </summary>
    /// <param name="next">The previous <see cref="BearerToken"/> resolver.</param>
    /// <param name="tokenType">The token type to match.</param>
    /// <param name="resolveForTokenType">The <see cref="BearerToken"/> resolver that is used when the context token type is equal to <paramref name="tokenType"/>.</param>
    /// <returns>A new <see cref="ResolveRequestDependency{T}"/> configured to use <paramref name="resolveForTokenType"/>.</returns>
    public static ResolveRequestDependency<BearerToken?> WhenTokenTypeIs(
        this ResolveRequestDependency<BearerToken?> next,
        BearerTokenType? tokenType,
        ResolveRequestDependency<BearerToken?> resolveForTokenType
        )
        => (scope, ct) => scope.ResolveOrDefault<BearerTokenType?>(ct)
            .BindAsync(type => type == tokenType
                ? resolveForTokenType(scope, ct)
                : next(scope, ct));

    /// <summary>
    /// Configure the client to use app access tokens for user authenticated endpoints that support prior authorization.
    /// </summary>
    /// <param name="client">The client to configure.</param>
    /// <returns>A new <see cref="TwitchClient"/> configured to enable prior authorization.</returns>
    public static TwitchClient UsePriorAuthorization(this TwitchClient client)
        => client.ConfigureFor<TwitchClient, BearerTokenType?>(
            (scope, ct) => scope.ResolveOrDefault<ITwitchRequestAuthenticationContext<TwitchIdentity>>(ct).MapAsync(context => context is ISupportPriorAuthorization),
            _ => (_, _) => ValueTask.FromResult<Validation<BearerTokenType?>>(BearerTokenType.AppAccessToken));
}
