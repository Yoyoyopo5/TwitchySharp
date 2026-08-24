namespace TwitchySharp.Api;
/// <summary>
/// Implements a handler for accepting a <see cref="TwitchRequest"/> command and returning a <see cref="TwitchResponse{T}"/>,
/// usually through a direct HTTP request to the Twitch API.
/// </summary>
public interface ITwitchClient
{
    /// <summary>
    /// Asynchronously send a <see cref="TwitchRequest"/> with a <typeparamref name="TResponseContent"/> response content type,
    /// returning a <see cref="TwitchResponse{TResponseContent}"/>.
    /// </summary>
    /// <typeparam name="TResponseContent">The response content type.</typeparam>
    /// <param name="request">The request to send.</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>A <see cref="Task"/> containing the response with a typed content property.</returns>
    Task<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(TwitchRequest<TResponseContent> request, CancellationToken ct = default);
}
