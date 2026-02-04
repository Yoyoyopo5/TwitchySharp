using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

public record SequentialClientIdentityResolver(IEnumerable<IResolveClientIdentity> ResolverChain) : IResolveClientIdentity
{
    /// <summary>
    /// Resolves to the first non-null <see cref="ClientIdentity"/> returned by the <see cref="ResolverChain"/>.
    /// </summary>
    /// <param name="request">The request to get a <see cref="ClientIdentity"/> for.</param>
    /// <returns>The first non-null <see cref="ClientIdentity"/> returned by the chain, or <see langword="null"/>.</returns>
    public ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default)
        => ResolveChain(request, ct);

    private async ValueTask<ClientIdentity?> ResolveChain(ITwitchRequest request, CancellationToken ct = default)
    {
        foreach (IResolveClientIdentity resolver in ResolverChain)
        {
            if (await resolver.GetClientId(request, ct).ConfigureAwait(false) is ClientIdentity client)
                return client;
        }
        return null;
    }
}
