using System;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Base class for requests using the <see href="https://dev.twitch.tv/docs/authentication/">Twitch Authentication API</see>.
/// </summary>
/// <typeparam name="TResponseContent"><inheritdoc/></typeparam>
public abstract record TwitchAuthorizationRequest<TResponseContent>
    : TwitchRequest<TResponseContent>
{
    /// <summary>
    /// The host for Twitch authorization requests.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>id.twitch.tv</c>. 
    /// Don't change this unless you have a special use case (e.g. testing).
    /// </remarks>
    public string Host { get; init; } = "id.twitch.tv";
    /// <summary>
    /// The base path for Twitch authoritzation requests.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>/oauth2</c>.
    /// Don't change this unless you have a special use case (e.g. testing).
    /// Request-specific paths are appended to this path.
    /// </remarks>
    public string BasePath { get; init; } = "/oauth2";
    /// <summary>
    /// The the authorization endpoint path.
    /// </summary>
    /// <remarks>
    /// Should be what comes after <c>/oauth2</c> in the request URL.
    /// </remarks>
    protected abstract string Path { get; }
    /// <summary>
    /// The request query parameters, if any.
    /// </summary>
    protected virtual HttpQueryParameters? QueryParameters { get; }
    public override Uri RequestUri => new UriBuilder()
    {
        Scheme = "https",
        Host = Host,
        Path = BasePath + Path,
        Query = QueryParameters?.ToString()
    }.Uri;
}
