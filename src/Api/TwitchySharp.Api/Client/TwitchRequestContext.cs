namespace TwitchySharp.Api;

/// <summary>
/// An extendable wrapper around a <see cref="TwitchRequest"/>.
/// </summary>
/// <remarks>
/// Allows for modifying how requests are converted to <see cref="HttpRequestMessage"/> at the pipeline level.
/// </remarks>
public record TwitchRequestContext
{
    /// <summary>
    /// The Twitch API request to send.
    /// </summary>
    public required TwitchRequest Request { get; init; }

    public static implicit operator TwitchRequestContext(TwitchRequest request)
      => new() { Request = request };
    /// <summary>
    /// Create an <see cref="HttpRequestMessage"/> from the context.
    /// </summary>
    /// <returns>An <see cref="HttpRequestMessage"/> representing the context that can be sent via an <see cref="HttpClient"/> instance.</returns>
    public virtual HttpRequestMessage ToHttpRequestMessage()
        => Request.ToHttpRequestMessage();
}
