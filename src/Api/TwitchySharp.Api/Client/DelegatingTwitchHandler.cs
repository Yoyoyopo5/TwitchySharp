namespace TwitchySharp.Api;

/// <summary>
/// Provides a partial definition of <see cref="ITwitchClient.SendAsync"/> that depends on another implementation of <see cref="ITwitchClient"/>.
/// </summary>
/// <remarks>
/// Implement this abstract class to configure the entire <see cref="ITwitchClient"/> send method.
/// The <see cref="TwitchClient"/> can be configured with instances of this type using <see cref="TwitchClient.Handlers"/>.
/// </remarks>
public abstract class DelegatingTwitchHandler
{
    private record Configured(ITwitchClient Client, DelegatingTwitchHandler Handler) : ITwitchClient
    {
        public Task<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(TwitchRequest<TResponseContent> request, CancellationToken ct = default)
            => Handler.SendAsync(Client, request, ct);
    }

    internal ITwitchClient ToClient(ITwitchClient client) => new Configured(client, this);

    /// <param name="client">The inner client. Use this to send the request.</param>
    /// <inheritdoc cref="ITwitchClient.SendAsync" />
    protected abstract Task<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(ITwitchClient client, TwitchRequest<TResponseContent> request, CancellationToken ct = default);
}
