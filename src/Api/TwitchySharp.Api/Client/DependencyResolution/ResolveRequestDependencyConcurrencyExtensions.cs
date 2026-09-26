using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

internal static class ResolveRequestDependencyConcurrencyExtensions
{
    public static ResolveRequestDependency<T> SerializeBy<T, TKey>(
        this ResolveRequestDependency<T> next,
        Func<TKey, CancellationToken, ValueTask<IAsyncDisposable>>? lockFactory = null
        )
        where TKey : class
    {
        lockFactory ??= ThreadSafety.CreateInMemoryLockProvider<TKey>();
        return (scope, ct) => scope.ResolveOrDefault<TKey?>(ct)
            .BindAsync(key => key is null
                ? next(scope, ct)
                : lockFactory(key, ct).AwaitUsing(() => next(scope, ct)));
    }

    public static ResolveRequestDependency<T> SerializeByValue<T, TKey>(
        this ResolveRequestDependency<T> next,
        Func<TKey, CancellationToken, ValueTask<IAsyncDisposable>>? lockFactory = null
        )
        where TKey : struct
    {
        lockFactory ??= ThreadSafety.CreateInMemoryLockProvider<TKey>();
        return (scope, ct) => scope.ResolveOrDefault<TKey?>(ct)
            .BindAsync(key => key is null
                ? next(scope, ct)
                : lockFactory(key.Value, ct).AwaitUsing(() => next(scope, ct)));
    }
}
