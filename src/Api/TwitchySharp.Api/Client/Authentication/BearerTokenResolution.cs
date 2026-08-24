namespace TwitchySharp.Api;

public static class BearerTokenResolution
{
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
             ValueTask.FromResult(new DependencyResult<BearerToken?>(context.Request is IAuthenticatedTwitchRequest<ITwitchRequestAuthenticationContext<TwitchIdentity>> request
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
    public static TwitchClient UseBearerToken(
        this TwitchClient client,
        BearerToken fixedToken
        )
        => client.ConfigureAsNullCoalesce((context, ct) => ValueTask.FromResult(new DependencyResult<BearerToken?>(fixedToken, context)));
}
