using System.Net.Http;
using System.Net.Http.Headers;

namespace TwitchySharp.Api;

internal static class HttpRequestMessageTwitchExtensions
{
    public static HttpRequestMessage AddTwitchAuthorizationHeaders(this HttpRequestMessage request, TwitchAuthorizationRequestOptions? auth)
    {
        if (auth is null)
            return request;
        request.Headers.Add("Client-Id", auth.ClientId);
        if (auth.BearerToken is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth.BearerToken);
        return request;
    }
}
