using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// A simple <see cref="IResolveClientIdentity"/> implementation that returns the same client identity for all requests.
/// </summary>
/// <param name="ClientId">The client identity to return for all requests, or <see langword="null"/> to not provide a fallback.</param>
public record SingleClientIdentityResolver(ClientIdentity? ClientId) : IResolveClientIdentity
{
    /// <inheritdoc/>
    public ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default)
        => ValueTask.FromResult(ClientId);
}
