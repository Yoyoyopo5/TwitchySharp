using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves the <see cref="ClientSecret"/> for a given <see cref="ClientId"/>.
/// </summary>
/// <remarks>
/// For simple single-client scenarios, use <see cref="SingleClientSecretResolver"/>.
/// </remarks>
public interface IResolveClientSecret
{
    /// <summary>
    /// Gets the client secret for the given client id.
    /// </summary>
    /// <param name="clientId">The client id to get the secret for.</param>
    /// <returns>
    /// The client secret for the given client id, or <see langword="null"/> if no secret is found.
    /// </returns>
    ValueTask<ClientSecret?> GetClientSecret(ClientId clientId, CancellationToken ct = default);
}
