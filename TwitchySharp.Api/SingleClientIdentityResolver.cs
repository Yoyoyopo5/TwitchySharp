using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// A simple <see cref="IResolveClientIdentity"/> implementation that returns the same client identity for all requests.
/// </summary>
/// <remarks>
/// This is suitable for most applications that use a single Twitch application client ID.
/// For multi-tenant scenarios where different requests may need different client IDs,
/// implement <see cref="IResolveClientIdentity"/> directly.
/// </remarks>
/// <param name="ClientId">The client identity to return for all requests, or <see langword="null"/> to not provide a fallback.</param>
public record SingleClientIdentityResolver(ClientIdentity? ClientId) : IResolveClientIdentity
{
    /// <inheritdoc/>
    public ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default)
        => ValueTask.FromResult(ClientId);
}
