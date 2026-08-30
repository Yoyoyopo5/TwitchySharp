namespace TwitchySharp.Api;

public static class BearerTokenResolution
{
    /// <summary>
    /// Set a <see cref="BearerToken"/> resolver for a specific <see cref="ITwitchRequestAuthenticationContext{T}"/>
    /// when the specified <paramref name="predicate"/> returns <see langword="true"/>.
    /// </summary>
    /// <typeparam name="TContext">The authentication context to set a <see cref="BearerToken"/> resolver for.</typeparam>
    /// <param name="next">The previous <see cref="BearerToken"/> resolver.</param>
    /// <param name="predicate">The condition that must be met in order for <paramref name="resolveForContext"/> to be evaluated.</param>
    /// <param name="resolveForContext">The <see cref="BearerToken"/> that is used when <paramref name="predicate"/> returns <see langword="true"/>.</param>
    /// <returns>A new <see cref="ResolveRequestDependency{T}"/> configured to use <paramref name="resolveForContext"/>.</returns>
    public static ResolveRequestDependency<BearerToken?> ResolveFor<TContext>(
        this ResolveRequestDependency<BearerToken?> next,
        Func<TContext, bool> predicate,
        ResolveRequestDependency<BearerToken?> resolveForContext
        )
        => (scope, ct)
            => scope.GetOrDefault<ITwitchRequestAuthenticationContext<TwitchIdentity>>(ct)
                .BindAsync((nextScope, authenticationContext) => authenticationContext is TContext match && predicate(match)
                    ? resolveForContext(nextScope, ct)
                    : next(nextScope, ct));

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
        => (scope, ct) => scope.GetOrDefault<BearerTokenType?>(ct)
            .BindAsync((scope, t) => t == tokenType
                ? resolveForTokenType(scope, ct)
                : next(scope, ct));

    /// <summary>
    /// Configure the client to use app access tokens for user authenticated endpoints that support prior authorization.
    /// </summary>
    /// <param name="client">The client to configure.</param>
    /// <returns>A new <see cref="TwitchClient"/> configured to enable prior authorization.</returns>
    public static TwitchClient UsePriorAuthorization(this TwitchClient client)
        => client.ConfigureFor<BearerTokenType?>(
            (scope, ct) => scope.GetOrDefault<ITwitchRequestAuthenticationContext<TwitchIdentity>>(ct).MapAsync(context => context is ISupportPriorAuthorization),
            _ => (scope, _) => ValueTask.FromResult(scope.ToResult<BearerTokenType?>(BearerTokenType.AppAccessToken)));

    /// <summary>
    /// Configure the <see cref="TwitchClient"/> to use the <see cref="ITwitchRequestAuthenticationContext{TIdentity}.BearerToken"/>
    /// from the <see cref="IAuthenticatedTwitchRequest"/> as its <c>Authorization</c> request header.
    /// </summary>
    /// <remarks>
    /// The previous configured <see cref="BearerToken"/> resolver will be tried before this one.
    /// </remarks>
    /// <param name="client">The client to configure</param>
    /// <returns>A new <see cref="TwitchClient"/> configured to use the <see cref="ITwitchRequestAuthenticationContext{TIdentity}.BearerToken"/>.</returns>
    public static TwitchClient UseBearerTokenFromRequestAuthenticationContext(
        this TwitchClient client
        )
        => client.ConfigureAsNullCoalesce((context, ct) =>
             ValueTask.FromResult(new RequestDependencyResult<BearerToken?>(context.Request is IAuthenticatedTwitchRequest<ITwitchRequestAuthenticationContext<TwitchIdentity>> request
                    ? request.AuthenticationContext.BearerToken
                    : null, context)));

    /// <summary>
    /// Configure the <see cref="TwitchClient"/> to use a fixed <see cref="BearerToken"/>
    /// as its <c>Authorization</c> request header.
    /// </summary>
    /// <remarks>
    /// The previous configured <see cref="BearerToken"/> resolver will be tried before this one.
    /// </remarks>
    /// <param name="client">The client to configure.</param>
    /// <param name="fixedToken">The token to use for all requests.</param>
    /// <returns>A new <see cref="TwitchClient"/> configured to use the <paramref name="fixedToken"/> for all requests.</returns>
    public static TwitchClient UseDefaultBearerToken(
        this TwitchClient client,
        BearerToken fixedToken
        )
        => client.ConfigureDefault(fixedToken);
}
