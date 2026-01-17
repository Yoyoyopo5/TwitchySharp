using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.Exceptions;
using TwitchySharp.Api.Extensions;
using TwitchySharp.Api.ResponseConverters;
using TwitchySharp.Shared;

namespace TwitchySharp.Api.Core;

public class TwitchClient(HttpClient httpClient, IConvertResponseContent? responseContentConverter = null, JsonSerializerOptions? requestContentSerializerOptions = null) : ITwitchClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConvertResponseContent _responseContentConverter = responseContentConverter ?? new AttributeFirstResponseContentConverter(new JsonResponseConverter(JsonConfig.ApiOptions));
    private readonly JsonSerializerOptions _requestContentSerializerOptions = requestContentSerializerOptions ?? JsonConfig.ApiOptions;

    public async ValueTask<ITwitchResponse> SendAsync(ITwitchRequest request, CancellationToken ct = default)
    {
        using HttpRequestMessage requestMessage = request
            .ToHttpRequestMessage(_requestContentSerializerOptions)
            .AddTwitchAuthorizationHeaders();
        using HttpResponseMessage response = await _httpClient.SendAsync(requestMessage, ct).ConfigureAwait(false);
        return response switch
        {
            { IsSuccessStatusCode: true } => new TwitchResponse
            {
                Request = request,
                StatusCode = response.StatusCode,
                RateLimitDetails = response.Headers.ToTwitchRateLimitDetails()
            },
            _ => throw await TwitchApiException.FromRequestResponseAsync(request, response, ct).ConfigureAwait(false)
            // We stick with exceptions here instead of a result return type just because HttpClient can
            // also throw many exceptions, and the response converter may also throw. So instead of mixing patterns,
            // lets just use the custom exception. A Result based client can always be added later that wraps
            // any exceptions thrown in processing.
        };
    }

    public async ValueTask<ITwitchResponse<TResponseContent>> SendAsync<TResponseContent>(
        ITwitchRequest<TResponseContent> request, 
        CancellationToken ct = default
        )
    {
        using HttpRequestMessage requestMessage = request
            .ToHttpRequestMessage(_requestContentSerializerOptions)
            .AddTwitchAuthorizationHeaders();
        using HttpResponseMessage response = await _httpClient.SendAsync(requestMessage, ct).ConfigureAwait(false);
        return response switch
        {
            { IsSuccessStatusCode: true } => new TwitchResponse<TResponseContent>
            {
                Request = request,
                StatusCode = response.StatusCode,
                RateLimitDetails = response.Headers.ToTwitchRateLimitDetails(),
                Content = await _responseContentConverter.Convert<TResponseContent>(response, ct).ConfigureAwait(false)
            },
            _ => throw await TwitchApiException.FromRequestResponseAsync(request, response, ct).ConfigureAwait(false)
        };
    }
}
