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

    public static HttpRequestMessage AddOrUpdateHeader(this HttpRequestMessage request, string key, string value)
    {
        request.Headers.AddOrUpdate(key, value);
        return request;
    }

    public static HttpRequestMessage SetAuthorizationBearer(this HttpRequestMessage request, string bearer)
    {
        request.Headers.Authorization = new("Bearer", bearer);
        return request;
    }
}
