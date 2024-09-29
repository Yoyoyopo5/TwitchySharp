using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TwitchySharp.Api.Helix;
/// <summary>
/// Represents GET request or request with no data or arbitrary string data
/// </summary>
/// <typeparam name="TResponse">The response type of the request.</typeparam>
public abstract class HelixApiRequest<TResponse>
    : TwitchApiRequest<TResponse>
{
    public sealed override string BaseUrl => "https://api.twitch.tv";
    public override HttpMethod Method => HttpMethod.Get;
    public override string? Data => null;
    public override string? ContentType => null;
    public HelixApiRequest(string path, string clientId, string accessToken)
        : base("/helix" + path)
    {
        ClientId = clientId;
        AccessToken = accessToken;
    }
}

/// <summary>
/// Represents POST request that contains JSON data, serialized from an instance of <typeparamref name="TRequestData"/>.
/// </summary>
/// <typeparam name="TResponse">The response type of the request.</typeparam>
/// <typeparam name="TRequestData">The type that will be serialized into the request data.</typeparam>
public abstract class HelixApiRequest<TResponse, TRequestData>
    : TwitchJsonApiRequest<TResponse, TRequestData>
{
    public sealed override string BaseUrl => "https://api.twitch.tv";
    public HelixApiRequest(string path, string clientId, string accessToken, TRequestData data)
        : base("/helix" + path, data)
    {
        ClientId = clientId;
        AccessToken = accessToken;
    }
}
