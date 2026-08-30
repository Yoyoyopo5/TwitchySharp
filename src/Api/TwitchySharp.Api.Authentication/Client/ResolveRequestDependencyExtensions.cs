namespace TwitchySharp.Api.Authentication;

public static class ResolveRequestDependencyExtensions
{
    public static ResolveRequestDependency<TDetails> WithCache<TKey, TDetails>(
        this ResolveRequestDependency<TDetails> next,
        ITwitchTokenCache<TKey, TDetails> cache,
        Func<TDetails, bool> isValid
        )
        => (context, ct) => context.GetOrDefault<TKey>(ct)
            .BindAsync(async (scope, key) => key is null
                ? scope.ToResult((TDetails?)default)
                : await cache.GetOrDefault(key, ct) is TDetails cachedDetails && isValid(cachedDetails)
                ? scope.ToResult(cachedDetails)
                : await next(scope, ct).MapAsync(async newDetails =>
                {
                    if (newDetails is not null)
                        await cache.Set(key, newDetails);
                    return newDetails;
                }));
}
