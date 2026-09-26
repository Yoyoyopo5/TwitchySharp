using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;
using TwitchySharp.Api.Authentication;

namespace TwitchySharp.Api.Tests.Integration.Client;

public static class RequestDependencyCache
{
    public static ValueTask<IRequestDependencyCache<TKey, TValue>> Create<TKey, TValue>(
            IEnumerable<KeyValuePair<TKey, TValue>>? tokens = null
            )
            where TKey : notnull
            where TValue : IAccessTokenDetails<TwitchIdentity>
            => tokens is null
                ? ValueTask.FromResult<IRequestDependencyCache<TKey, TValue>>(new InMemoryConcurrentCache<TKey, TValue>())
                : tokens.Aggregate(
                ValueTask.FromResult<IRequestDependencyCache<TKey, TValue>>(new InMemoryConcurrentCache<TKey, TValue>()),
                async (current, next) => await (await current).Set(
                    next.Key,
                    next.Value,
                    TestContext.Current.CancellationToken
                    ));
}
