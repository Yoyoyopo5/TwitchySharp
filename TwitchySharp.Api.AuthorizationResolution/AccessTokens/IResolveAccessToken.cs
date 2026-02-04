using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

public interface IResolveAccessToken<in TKey>
{
    /// <summary>
    /// Resolves an <see cref="AccessToken"/> based on the provided <see cref="TKey"/>.
    /// </summary>
    /// <param name="request">The request to get an access token for.</param>
    /// <returns>An <see cref="AccessTokenResolutionResult"/> resolved from the <see cref="TKey"/>. This can be pattern matched to determine result type.</returns>
    ValueTask<AccessTokenResolutionResult> GetToken(TKey key, CancellationToken ct = default);
}