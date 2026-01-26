using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

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
    /// <param name="ct">Cancellation token.</param>
    /// <returns>
    /// The client identity to use, or <see langword="null"/> if no client identity should be applied.
    /// When returned, this value is used as a fallback if the request's <see cref="IRequireAuthorization.Identity"/>
    /// does not already have a <see cref="TwitchApiIdentity.ClientId"/> set.
    /// </returns>
    ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default);
}
