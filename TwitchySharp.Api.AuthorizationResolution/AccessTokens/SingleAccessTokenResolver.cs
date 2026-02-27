using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves a pre-configured default <see cref="AccessToken"/> for every <see cref="TKey"/>.
/// </summary>
/// <param name="Token">The <see cref="AccessToken"/> to use for all requests.</param>
public record SingleAccessTokenResolver<TKey, TToken>(TToken Token) : IResolveAccessToken<TKey>
    where TToken : AccessToken
{
    private readonly AccessTokenDetailsResolutionResult.Available<TToken> _tokenResult = new(Token);
    public ValueTask<AccessTokenDetailsResolutionResult> ResolveAsync(TKey key, CancellationToken ct = default)
        => ValueTask.FromResult(_tokenResult as AccessTokenDetailsResolutionResult);
}