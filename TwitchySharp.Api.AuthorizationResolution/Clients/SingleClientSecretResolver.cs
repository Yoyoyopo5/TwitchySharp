using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// A simple <see cref="IResolveClientSecret"/> implementation that returns the same client secret
/// for a specific client id.
/// </summary>
/// <remarks>
/// For more complex scenarios like multi-tenant applications or live rotating secrets,
/// you can implement <see cref="IResolveClientSecret"/> directly.
/// </remarks>
/// <param name="ClientId">The client id this resolver handles.</param>
/// <param name="ClientSecret">The client secret to return for matching requests.</param>
public record SingleClientSecretResolver(ClientId ClientId, ClientSecret ClientSecret)
    : IResolveClientSecret
{
    /// <inheritdoc/>
    public ValueTask<ClientSecret?> GetClientSecret(ClientId clientId, CancellationToken ct = default)
        => ValueTask.FromResult<ClientSecret?>(clientId == ClientId ? ClientSecret : null);
}
