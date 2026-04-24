using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;
/// <summary>
/// Implements a handler for accepting <see cref="TwitchRequest"/> instances and returning <see cref="TwitchResponse"/> instances,
/// usually through a direct HTTP request to the Twitch API.
/// </summary>
public interface ITwitchClient
{
    /// <summary>
    /// Send a request to the Twitch API with an untyped response content type.
    /// </summary>
    /// <param name="request">The Twitch API request.</param>
    /// <returns>The API response with untyped content.</returns>
    ValueTask<TwitchResponse> SendAsync(TwitchRequest request, CancellationToken ct = default);
    /// <summary>
    /// Send a request to the Twitch API with a strongly typed response content type.
    /// </summary>
    /// <typeparam name="TResponseContent">The response content type.</typeparam>
    /// <param name="request">The Twitch API request.</param>
    /// <returns>The API response with a typed content property.</returns>
    ValueTask<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(TwitchRequest<TResponseContent> request, CancellationToken ct = default);
}
