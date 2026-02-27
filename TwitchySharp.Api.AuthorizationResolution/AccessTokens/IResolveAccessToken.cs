using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

public interface IResolveAccessToken<in TKey> : IResolveAsync<TKey, AccessTokenDetailsResolutionResult>
{
    /// <summary>
    /// Resolves an <see cref="AccessToken"/> based on the provided <typeparamref name="TKey"/>.
    /// </summary>
    /// <param name="request">The request to get an access token for.</param>
    /// <returns>An <see cref="AccessTokenDetailsResolutionResult"/> resolved from the <typeparamref name="TKey"/>. This can be pattern matched to determine result type.</returns>
    new ValueTask<AccessTokenDetailsResolutionResult> ResolveAsync(TKey key, CancellationToken ct = default);
}