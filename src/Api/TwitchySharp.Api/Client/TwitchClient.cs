namespace TwitchySharp.Api;

/// <summary>
/// A Twitch API client that uses a <see cref="SendTwitchRequest"/> function to send requests.
/// </summary>
public record TwitchClient : ITwitchClient
{
    /// <summary>
    /// Create a default Twitch API client that sends requests via the <paramref name="httpClient"/>.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> to send API requests with.</param>
    /// <returns>The default <see cref="TwitchClient"/>.</returns>
    public static TwitchClient CreateDefault(HttpClient httpClient)
        => new() { RequestHandler = TwitchRequestSender.CreateDefault(httpClient) };

    /// <summary>
    /// The request handler of the client.
    /// </summary>
    /// <remarks>
    /// Use this to configure custom middleware on the client or otherwise modify the request pipeline.
    /// If you have no clue what you're doing and just want something that works, use the <see cref="TwitchClient.CreateDefault(HttpClient)"/> static method.
    /// </remarks>
    public required SendTwitchRequest RequestHandler { get; init; }
    /// <inheritdoc/>
    public async Task<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(TwitchRequest<TResponseContent> request, CancellationToken ct = default) => (TwitchResponse<TResponseContent>)await RequestHandler(request, ct).ConfigureAwait(false);
}

public static class TwitchClientExtensions
{
    public static TwitchClient With(this TwitchClient client, Func<SendTwitchRequest, SendTwitchRequest> addMiddleware)
        => client with { RequestHandler = addMiddleware(client.RequestHandler) };
}
