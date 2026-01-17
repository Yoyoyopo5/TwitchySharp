using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace TwitchySharp.Api.Core;

public record TwitchRequest : ITwitchRequest
{
    public HttpMethod Method { get; init; } = HttpMethod.Get;
    public Uri RequestUri { get; init; } = new("https://api.twitch.tv/");
    public string? ClientId { get; init; }
    public string? AccessToken { get; init; }
    public object? ContentObject { get; init; }
    public HttpContent? Content { get; init; }
    public virtual HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions)
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = Method,
            RequestUri = RequestUri,
            Content = Content ?? ContentObject switch
            {
                { } content => JsonContent.Create(content, MediaTypeHeaderValue.Parse("application/json"), serializerOptions),
                _ => null
            }
        };
        httpRequest.Options.Set(TwitchRequestOptionsKeys.Authorization, new TwitchAuthorizationRequestOptions
        {
            ClientId = ClientId,
            AccessToken = AccessToken
        });
        return httpRequest;
    }
}

public record TwitchRequest<TResponseContent> : TwitchRequest, ITwitchRequest<TResponseContent>;
