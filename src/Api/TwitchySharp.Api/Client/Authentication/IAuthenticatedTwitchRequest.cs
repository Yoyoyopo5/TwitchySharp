namespace TwitchySharp.Api;
/// <summary>
/// <inheritdoc cref="IAuthenticatedTwitchRequest"/>
/// </summary>
/// <typeparam name="TContext">The type of <see cref="ITwitchRequestAuthenticationContext{TIdentity}"/> that the request uses.</typeparam>
public interface IAuthenticatedTwitchRequest<out TContext> : IAuthenticatedTwitchRequest
    where TContext : ITwitchRequestAuthenticationContext<TwitchIdentity>
{
    /// <inheritdoc cref="IAuthenticatedTwitchRequest.AuthenticationContext"/>
    new TContext AuthenticationContext { get; }
    ITwitchRequestAuthenticationContext<TwitchIdentity> IAuthenticatedTwitchRequest.AuthenticationContext => AuthenticationContext;
}

/// <summary>
/// A Twitch request that requires the ClientId and Authorization headers to be set.
/// </summary>
public interface IAuthenticatedTwitchRequest
{
    /// <summary>
    /// The authentication context to use for the request.
    /// </summary>
    ITwitchRequestAuthenticationContext<TwitchIdentity> AuthenticationContext { get; }
}
