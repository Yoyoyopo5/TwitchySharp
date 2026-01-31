using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Reads the <see cref="IRequireAuthorization.Identity"/> property from <see cref="ITwitchRequest"/> instances
/// that implement <see cref="IRequireAuthorization"/>, and returns a <see cref="ClientIdentity"/> with the
/// <see cref="TwitchApiIdentity.ClientId"/>, if it exists on the request.
/// </summary>
public record ConfiguredClientIdentityResolver() : IResolveClientIdentity
{
    public ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default)
        => ValueTask.FromResult<ClientIdentity?>(request switch
        {
            IRequireAuthorization requiresAuthorization => requiresAuthorization.Identity.ClientId,
            _ => null
        });
}
