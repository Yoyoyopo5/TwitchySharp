using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

public record StoredTokenResolver<TToken, TKey, TDetails>(ITokenStore<TToken, TKey, TDetails> TokenStore)
    : IResolveAccessToken<TKey>
    where TToken : AccessToken
    where TDetails : IAccessTokenDetails
{
    public async ValueTask<AccessTokenDetailsResolutionResult> ResolveAsync(TKey key, CancellationToken ct = default)
        => await TokenStore.GetTokenDetails(key, ct) switch
        {
            TDetails found => new AccessTokenDetailsResolutionResult.Available<TDetails>(found),
            _ => AccessTokenDetailsResolutionResult.Unavailable.Instance
        };
}