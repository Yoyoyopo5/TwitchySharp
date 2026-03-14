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
    public virtual TwitchRequestAuthorizationContext AuthorizationContext { get; }
        = new() { Identity = TwitchIdentity.Default.Instance };
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
