using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using TwitchySharp.Api.ApiResponseConverters;

namespace TwitchySharp.Api;
/// <summary>
/// A utility class used to send <see cref="TwitchApiRequest{TResponse}"/> to Twitch.
/// </summary>
/// <param name="twitchClient">The client to use.</param>
public class TwitchApi(TwitchHttpClient twitchClient)
{
    private readonly IConvertApiResponse _defaultConverter = new JsonResponseConverter(JsonConfig.ApiOptions);
    /// <summary>
    /// Sends an API request to Twitch and returns the response.
    /// </summary>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <param name="request">The request to send to Twitch.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>
    /// A <see cref="ValueTask"/> containing the API response converted to type <typeparamref name="TResponse"/>.
    /// </returns>
    /// <inheritdoc cref="TwitchHttpClient.SendAsync{TResponse}(TwitchApiRequest{TResponse}, IConvertApiResponse, CancellationToken)" path="/exception"/>
    public ValueTask<TResponse> SendRequestAsync<TResponse>(TwitchApiRequest<TResponse> request, CancellationToken ct = default)
        => twitchClient.SendAsync(request, GetConverter<TResponse>(), ct);

    private IConvertApiResponse GetConverter<TResponse>()
        => typeof(TResponse).GetCustomAttribute<ApiConverterAttribute>()?.CreateConverter() ?? _defaultConverter;
}
