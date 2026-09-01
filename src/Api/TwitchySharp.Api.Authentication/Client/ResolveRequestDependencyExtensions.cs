using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Authentication;

public static class ResolveRequestDependencyExtensions
{
    public static ResolveRequestDependency<TDetails?> WithCache<TKey, TDetails>(
        this ResolveRequestDependency<TDetails?> next,
        ITwitchTokenCache<TKey, TDetails> cache,
        Func<TDetails, bool> isValid
        )
        => (scope, ct) => scope.ResolveOrDefault<TKey>(ct)
            .BindAsync(async key => key is null
                ? (TDetails?)default
                : await cache.GetOrDefault(key, ct) is TDetails cachedDetails && isValid(cachedDetails)
                ? cachedDetails
                : await next(scope, ct).MapAsync(async newDetails =>
                {
                    if (newDetails is not null)
                        await cache.Set(key, newDetails);
                    return newDetails;
                }));
}
