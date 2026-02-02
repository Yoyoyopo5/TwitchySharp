using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves the <see cref="ClientIdentity"/> to use for a given request.
/// </summary>
/// <remarks>
/// This interface allows for dynamic client identity resolution based on the request context.
/// For simple single-client scenarios, use <see cref="SingleClientIdentityResolver"/>.
/// For multi-tenant scenarios, implement this interface to return different client identities
/// based on request properties.
/// </remarks>
public interface IResolveClientIdentity
{
    /// <summary>
    /// Gets the client identity to use for the given request.
    /// </summary>
    /// <param name="request">The request that needs a client identity.</param>
    /// <returns>
    /// The client identity to use for the request, if any.
    /// </returns>
    ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default);
}
