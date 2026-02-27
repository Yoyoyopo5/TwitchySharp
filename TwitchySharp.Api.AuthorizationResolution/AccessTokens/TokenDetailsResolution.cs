namespace TwitchySharp.Api.AuthorizationResolution;

public delegate ValueTask<AccessTokenDetailsResolutionResult> AccessTokenDetailsResolver<TKey>(TKey key, CancellationToken ct = default);
public static partial class TokenDetailsResolution
{
    /// <summary>
    /// Use the <paramref name="refresher"/> if the token details resolve to the access token details expected by the refresher.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TIdentity"></typeparam>
    /// <typeparam name="TToken"></typeparam>
    /// <param name="refresher"></param>
    /// <returns></returns>
    public static Func<AccessTokenDetailsResolver<TKey>, AccessTokenDetailsResolver<TKey>> UseRefresh<TKey, TDetails>(AccessTokenRefresher<TDetails> refresher)
        where TDetails : IAccessTokenDetails
        => next => async (key, ct) =>
        await next(key, ct) switch
        {
            IHaveAccessTokenDetails<TDetails> tokenResult => (await refresher(tokenResult.AccessTokenDetails, ct)).ToResolutionResult(),
            AccessTokenDetailsResolutionResult result => result
        };
}