using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves <see cref="AccessToken"/>s by iterating through a chain of <see cref="IResolveAccessToken{TKey}"/>s,
/// with the first non-null <see cref="AccessToken"/> returned, if any.
/// </summary>
/// <param name="ResolverChain">The chain of resolvers to use.</param>
[CollectionBuilder(typeof(SequentialAccessTokenResolverBuilder), nameof(SequentialAccessTokenResolverBuilder.Create))]
public record SequentialAccessTokenResolver<TKey>(IEnumerable<IResolveAccessToken<TKey>> ResolverChain) 
    : IResolveAccessToken<TKey>, IEnumerable<IResolveAccessToken<TKey>>
{
    // IEnumerable necessary to use C#12 collection builder feature.
    IEnumerator<IResolveAccessToken<TKey>> IEnumerable<IResolveAccessToken<TKey>>.GetEnumerator() => ResolverChain.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ResolverChain.GetEnumerator();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="key"><inheritdoc/></param>
    /// <returns>The first non-null <see cref="AccessToken"/> returned by a resolver in the <see cref="ResolverChain"/>.</returns>
    public ValueTask<AccessTokenResolutionResult> GetToken(TKey key, CancellationToken ct = default)
        => ResolveChain(key, ct);

    private async ValueTask<AccessTokenResolutionResult> ResolveChain(TKey key, CancellationToken ct = default)
    {
        AccessTokenResolutionResult result = AccessTokenResolutionResult.Unavailable.Instance;
        foreach (IResolveAccessToken<TKey> resolver in ResolverChain)
        {
            result = await resolver.GetToken(key, ct);
            if (result is IHaveAccessToken<AccessToken>)
                return result; // Short circuit on first available token.
        }
        return result; // Returns last unavailable result or the default unavailable result if there are no resolvers.
    }
}

internal static class SequentialAccessTokenResolverBuilder
{
    public static SequentialAccessTokenResolver<TKey> Create<TKey>(ReadOnlySpan<IResolveAccessToken<TKey>> resolvers)
        => new(resolvers.ToArray());
}