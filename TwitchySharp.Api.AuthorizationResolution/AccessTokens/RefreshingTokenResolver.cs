namespace TwitchySharp.Api.AuthorizationResolution;

public record RefreshingAccessTokenResolver<TKey, TDetails>(IRefreshAccessToken<TDetails> Refresher)
    : DelegatingResolver<TKey, AccessTokenDetailsResolutionResult>
    where TDetails : IAccessTokenDetails
{
    public override async ValueTask<AccessTokenDetailsResolutionResult> ResolveAsync(TKey key, CancellationToken ct = default)
    {
        AccessTokenDetailsResolutionResult innerResult = await base.ResolveAsync(key, ct);
        if (innerResult is not AccessTokenDetailsResolutionResult.Expired<TDetails> expiredResult)
            return innerResult;
        return (await Refresher.Refresh(expiredResult, ct)).ToResolutionResult();
    }
}

public static class RefreshingAccessTokenResolverChainExtensions
{
    public static ResolverChain<TKey, AccessTokenDetailsResolutionResult> ThenRefreshExpired<TKey, TDetails>(
        this ResolverChain<TKey, AccessTokenDetailsResolutionResult> chain,
        IRefreshAccessToken<TDetails> refresher
        )
        where TDetails : IAccessTokenDetails
        => chain.Then(prev => new RefreshingAccessTokenResolver<TKey, TDetails>(refresher) { InnerResolver = prev });
}
