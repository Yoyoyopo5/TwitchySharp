using System.Net.Http.Headers;

namespace TwitchySharp.Api;

internal static class HttpRequestHeadersExtensions
{
    public static HttpRequestHeaders AddOrUpdate(this HttpRequestHeaders headers, string key, string value)
    {
        headers.Remove(key);
        headers.Add(key, value);
        return headers;
    }
}
