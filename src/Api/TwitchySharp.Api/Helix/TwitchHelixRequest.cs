using System;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix;
/// <summary>
/// Used to create a request to a Twitch Helix API endpoint.
/// </summary>
/// <typeparam name="TResponseContent">The response content type of the request.</typeparam>
public abstract record TwitchHelixRequest<TResponseContent>
    : TwitchRequest<TResponseContent>, IAuthorizedTwitchRequest
{
    /// <summary>
    /// The host for the Helix request.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>api.twitch.tv</c>.
    /// Don't change this unless you have a special use case (e.g. testing).
    /// </remarks>
    public string Host { get; init; } = "api.twitch.tv";
    /// <summary>
    /// The base path for the Helix request.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>/helix</c>.
    /// Don't change this unless you have a special use case (e.g. testing).
    /// Request-specific endpoint paths will be appended to this when forming the <see cref="RequestUri"/>.
    /// </remarks>
    public string BasePath { get; init; } = "/helix";
    /// <summary>
    /// The path of the specific Helix endpoint.
    /// </summary>
    /// <remarks>
    /// This is appended to the <see cref="BasePath"/> when forming the <see cref="RequestUri"/>.
    /// </remarks>
    protected abstract string Path { get; }
    /// <summary>
    /// The authorization context for the request.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Contains request identity, required scopes, and an optional access token.
    /// This context is used to set the required Twitch authorization request headers.
    /// </para>
    /// <para>
    /// Typically, you do not need to set this, as it is configured automatically by the request itself.
    /// You may set this to override a request's default context or assign a specific access token the request should use.
    /// </para>
    /// </remarks>
    public TwitchRequestAuthorizationContext AuthorizationContext
    {
        get => _configuredAuthorizationContext ?? DefaultAuthorizationContext;
        init => _configuredAuthorizationContext = value;
    }
    private TwitchRequestAuthorizationContext? _configuredAuthorizationContext = null;
    protected virtual TwitchRequestAuthorizationContext DefaultAuthorizationContext { get; }
        = new() { Identity = TwitchIdentity.Client.Default };
    /// <summary>
    /// Query parameters for the request.
    /// </summary>
    protected virtual HttpQueryParameters? QueryParameters { get; }
    /// <inheritdoc/>
    public override Uri RequestUri => new UriBuilder
    {
        Scheme = "https",
        Host = Host,
        Path = BasePath + Path,
        Query = QueryParameters?.ToString()
    }.Uri;
}
