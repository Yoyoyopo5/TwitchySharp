using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.ResponseConverters;
using TwitchySharp.Shared;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api;
/// <summary>
/// The default Twitch API client implementation.
/// </summary>
/// <remarks>
/// Sends <see cref="ITwitchRequest"/>s with a provided <see cref="HttpClient"/>.
/// Handles adding required authorization headers and deserializing responses into response types.
/// </remarks>
/// <param name="httpClient">The client to use.</param>
/// <param name="clientConfig">
/// The client configuration to use. 
/// Use the <see cref="SingleClientConfiguration"/> for a simple static client id (fine for most cases).
/// </param>
/// <param name="authorizer">The request authorizer to use. This is required for Helix endpoints.</param>
/// <param name="responseContentConverter">
/// The response content converter.
/// Defaults to <see cref="AttributeFirstResponseContentConverter"/> if left <see langword="null"/>.
/// Don't change this unless you know what you're doing.
/// </param>
/// <param name="requestContentSerializerOptions">
/// The JSON serializer options to use when serializing <see cref="ITwitchRequest"/>s into <see cref="HttpRequestMessage"/>s.
/// Defaults to <see cref="JsonConfig.ApiOptions"/> if left <see langword="null"/>.
/// Don't change this unless you know what you're doing.
/// </param>
public class TwitchClient(HttpClient httpClient, IClientConfiguration clientConfig, IAuthorizeTwitchRequest? authorizer, IConvertResponseContent? responseContentConverter = null, JsonSerializerOptions? requestContentSerializerOptions = null) : ITwitchClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IClientConfiguration _clientConfig = clientConfig;
    private readonly IAuthorizeTwitchRequest? _authorizer = authorizer;
    private readonly IConvertResponseContent _responseContentConverter = responseContentConverter ?? new AttributeFirstResponseContentConverter(new JsonResponseConverter(JsonConfig.ApiOptions));
    private readonly JsonSerializerOptions _requestContentSerializerOptions = requestContentSerializerOptions ?? JsonConfig.ApiOptions;

    public async ValueTask<ITwitchResponse> SendAsync(ITwitchRequest request, CancellationToken ct = default)
    {
        using HttpResponseMessage response = await ConfigureAndSend(request, ct).ConfigureAwait(false);
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
        using HttpResponseMessage response = await ConfigureAndSend(request, ct).ConfigureAwait(false);
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

    private async ValueTask<HttpResponseMessage> ConfigureAndSend(ITwitchRequest request, CancellationToken ct = default)
    {
        using HttpRequestMessage requestMessage = request.ToHttpRequestMessage(_requestContentSerializerOptions);
        if (_authorizer is not null && request is IRequireAuthorization needsAuthorization)
        {
            ClientIdentity? client = await _clientConfig.GetClientId(request, ct);
            // WithClientFallback returns a copy of the original request type, preserving full context
            // for custom IAuthorizeTwitchRequest implementations that may need endpoint-specific data.
            IRequireAuthorization authContext = needsAuthorization.WithClientFallback(client);
            requestMessage.AddTwitchAuthorizationHeaders(await _authorizer.GetAuthorization(authContext, ct).ConfigureAwait(false));
        }
        return await _httpClient.SendAsync(requestMessage, ct).ConfigureAwait(false);
    }
}