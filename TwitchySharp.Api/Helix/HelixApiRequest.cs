using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TwitchySharp.Api.Helix;
/// <summary>
/// Used to create a request to a Twitch Helix API endpoint.
/// </summary>
/// <remarks>
/// Defaults to <see cref="HttpMethod.Get"/>.
/// </remarks>
/// <typeparam name="TResponse">The response type of the request.</typeparam>
public abstract class HelixApiRequest<TResponse>
    : TwitchApiRequest<TResponse>
{
    public sealed override string BaseUrl => "https://api.twitch.tv";
    public override HttpMethod Method => HttpMethod.Get;
    public override string? Data => null;
    public override string? ContentType => null;
    /// <summary>
    /// <inheritdoc cref="HelixApiRequest{TResponse}"/>
    /// </summary>
    /// <param name="path">The path of the endpoint (after <c>/helix</c>).</param>
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An access token used to authorize the request. This is used in the <c>Authorization</c> header.</param>
    public HelixApiRequest(string path, string clientId, string accessToken)
        : base("/helix" + path)
    {
        ClientId = clientId;
        AccessToken = accessToken;
    }
}

/// <summary>
/// <inheritdoc cref="HelixApiRequest{TResponse}"/>
/// </summary>
/// <remarks>
/// Defaults to <see cref="HttpMethod.Post"/>.
/// A provided instance of <typeparamref name="TRequestData"/> will be serialized as the request body.
/// </remarks>
/// <typeparam name="TResponse">The response type of the request.</typeparam>
/// <typeparam name="TRequestData">The type that will be serialized into the request data.</typeparam>
public abstract class HelixApiRequest<TResponse, TRequestData>
    : TwitchJsonApiRequest<TResponse, TRequestData>
{
    public sealed override string BaseUrl => "https://api.twitch.tv";
    /// <summary>
    /// <inheritdoc cref="HelixApiRequest{TResponse, TRequestData}"/>
    /// </summary>
    /// <param name="path">The path of the endpoint (after <c>/helix</c>).</param>
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An access token used to authorize the request. This is used in the <c>Authorization</c> header.</param>
    /// <param name="data">The data to serialize into the body of the request.</param>
    public HelixApiRequest(string path, string clientId, string accessToken, TRequestData data)
        : base("/helix" + path, data)
    {
        ClientId = clientId;
        AccessToken = accessToken;
    }
}
