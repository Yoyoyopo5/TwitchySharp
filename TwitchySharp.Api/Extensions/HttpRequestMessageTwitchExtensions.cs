using System.Net.Http;
using System.Net.Http.Headers;
using TwitchySharp.Api.Core;

namespace TwitchySharp.Api.Extensions;

internal static class HttpRequestMessageTwitchExtensions
{
    public static HttpRequestMessage AddTwitchAuthorizationHeaders(this HttpRequestMessage request)
    {
        if (request.Options.TryGetValue(TwitchRequestOptionsKeys.Authorization, out TwitchAuthorizationRequestOptions? authOptions))
        {
            if (!string.IsNullOrEmpty(authOptions.AccessToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authOptions.AccessToken);
            if (!string.IsNullOrEmpty(authOptions.ClientId))
                request.Headers.Add("Client-Id", authOptions.ClientId);
        }
        return request;
    }
}
