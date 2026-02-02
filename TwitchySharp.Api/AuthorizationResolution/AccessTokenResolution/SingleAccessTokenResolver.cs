using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Resolves a pre-configured default <see cref="AccessToken"/> for every <see cref="ITwitchRequest"/>.
/// </summary>
/// <param name="Token">The <see cref="AccessToken"/> to use for all requests.</param>
public record SingleAccessTokenResolver(AccessToken Token) : ITokenResolver
{
    public ValueTask<AccessToken?> GetToken(ITwitchRequest request, CancellationToken ct = default)
        => ValueTask.FromResult<AccessToken?>(Token);
}
