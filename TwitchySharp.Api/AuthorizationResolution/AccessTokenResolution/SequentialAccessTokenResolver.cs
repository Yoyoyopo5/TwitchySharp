using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Resolves <see cref="AccessToken"/>s by iterating through a chain of <see cref="ITokenResolver"/>s,
/// with the first non-null <see cref="AccessToken"/> returned, if any.
/// </summary>
/// <param name="ResolverChain">The chain of resolvers to use.</param>
public record SequentialAccessTokenResolver(IEnumerable<ITokenResolver> ResolverChain) : ITokenResolver
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="request"><inheritdoc/></param>
    /// <returns>The first non-null <see cref="AccessToken"/> returned by a resolver in the <see cref="ResolverChain"/>.</returns>
    public ValueTask<AccessToken?> GetToken(ITwitchRequest request, CancellationToken ct = default)
        => ResolveChain(request, ct);

    private async ValueTask<AccessToken?> ResolveChain(ITwitchRequest request, CancellationToken ct = default)
    {
        foreach (ITokenResolver resolver in ResolverChain)
        {
            if (await resolver.GetToken(request, ct) is AccessToken token)
                return token;
        }
        return null;
    }
}