namespace TwitchySharp.Api;
/// <summary>
/// Implements a handler for accepting <see cref="TwitchRequest"/> instances and returning <see cref="TwitchResponse"/> instances,
/// usually through a direct HTTP request to the Twitch API.
/// </summary>
public interface ITwitchClient
{
    // Our hand is forced to use an interface here because a delegate type param cannot be determined at call time.
    // The typed method here should point to an untyped request handler delegate (with a cast).
    /// <summary>
    /// Send a request to the Twitch API with a strongly typed response content type.
    /// </summary>
    /// <typeparam name="TResponseContent">The response content type.</typeparam>
    /// <param name="request">The Twitch API request.</param>
    /// <returns>The API response with a typed content property.</returns>
    Task<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(TwitchRequest<TResponseContent> request, CancellationToken ct = default);
}
