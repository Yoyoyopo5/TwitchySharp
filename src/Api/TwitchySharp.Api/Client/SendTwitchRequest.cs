namespace TwitchySharp.Api;

/// <summary>
/// Sends a single Twitch API request.
/// </summary>
/// <param name="context">The request context to use.</param>
/// <param name="ct">Cancellation token.</param>
/// <returns>A <see cref="Task"/> containing the <see cref="TwitchResponse"/> for the request.</returns>
public delegate Task<TwitchResponse> SendTwitchRequest(TwitchRequestContext context, CancellationToken ct = default);

public static class TwitchRequestSender
{
    /// <summary>
    /// Create a default <see cref="SendTwitchRequest"/> pipeline using the <paramref name="httpClient"/> to send requests.
    /// </summary>
    /// <param name="httpClient">The client to send requests with.</param>
    /// <returns>The default Twitch API request pipeline.</returns>
    public static SendTwitchRequest CreateDefault(HttpClient httpClient)
        => async (context, ct) =>
        {
            using HttpRequestMessage request = context.ToHttpRequestMessage();
            using HttpResponseMessage response = await httpClient.SendAsync(request, ct).ConfigureAwait(false);

            return response switch
            {
                { IsSuccessStatusCode: true } => await context.Request.CreateResponse(response, ct),
                _ => throw await TwitchApiException.FromRequestResponseAsync(context.Request, response, ct).ConfigureAwait(false)
            };
        };
}
