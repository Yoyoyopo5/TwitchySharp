using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix;
/// <summary>
/// Used to create a request to a Twitch Helix API endpoint.
/// </summary>
/// <typeparam name="TResponseContent">The response content type of the request.</typeparam>
public abstract record TwitchHelixRequest<TResponseContent>
    : TwitchRequest<TResponseContent>, IRequireAuthorization
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
    /// <inheritdoc/>
    /// </summary>
    /// <remarks>
    /// Generally, this is set automatically by specific request types and does not need to be configured.
    /// However, you can override the identity for setting a specific <see cref="ClientIdentity"/> or <see cref="UserIdentity"/> to make the request as.
    /// This identity will always override any default identity set by the individual request type.
    /// </remarks>
    public TwitchApiIdentity Identity
    {
        get => _configuredIdentity ?? DefaultIdentity; // Allow override of default identity.
        init => _configuredIdentity = value;
    }
    private TwitchApiIdentity? _configuredIdentity;
    /// <summary>
    /// The default identity to use for the request.
    /// </summary>
    protected virtual TwitchApiIdentity DefaultIdentity { get; } = TwitchApiIdentity.Default;
    /// <inheritdoc/>
    public virtual IReadOnlySet<Scope> ValidScopes { get; } = ImmutableHashSet<Scope>.Empty;
    /// <inheritdoc/>
    public virtual AccessToken? OverrideAccessToken { get; init; }
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
