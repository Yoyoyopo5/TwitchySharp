using System.Net.Http;
using System.Net.Http.Headers;

namespace TwitchySharp.Api;

internal static class HttpRequestMessageTwitchExtensions
{
    public static HttpRequestMessage AddTwitchAuthorizationHeaders(this HttpRequestMessage request, TwitchAuthorizationHeaders auth)
    {
        if (auth.ClientId is { Value.Length: > 0 } clientId)
            request.Headers.Add("Client-Id", clientId);
        if (auth.BearerToken is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth.BearerToken.Value);
        return request;
    }
}
