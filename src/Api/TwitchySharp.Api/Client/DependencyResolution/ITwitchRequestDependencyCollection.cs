using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

public interface ITwitchRequestDependencyCollection
    : ITwitchRequestDependencyCollection<ITwitchRequestDependencyCollection>;

public interface ITwitchRequestDependencyCollection<out TSelf>
    where TSelf : ITwitchRequestDependencyCollection<TSelf>
{
    TSelf SetResolver<T>(ResolveRequestDependency<T> resolve);
    ResolveRequestDependency<T>? GetResolver<T>();
}

public static class ITwitchRequestDependencyCollectionExtensions
{
    public static TCollection SetResolver<TCollection, T>(
        this TCollection dc,
        Func<ITwitchRequestDependencyScope, Validation<T>> resolve)
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver((scope, ct) => ValueTask.FromResult(resolve(scope)));

    public static TCollection SetResolver<TCollection, T>(
        this TCollection dc,
        Func<ITwitchRequestDependencyScope, T> resolve
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver((scope, ct) => ValueTask.FromResult<Validation<T>>(resolve(scope)));

    public static TCollection From<TCollection, T, TFrom>(
        this TCollection resolvers,
        Func<TFrom?, T> select
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => resolvers.SetResolver<T>((context, ct) =>
            context.ResolveOrDefault<TFrom>(ct).MapAsync(f => select(f)));

    public static TCollection FromRequest<TCollection, T>(
        this TCollection resolvers,
        Func<TwitchRequest, T> select
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => resolvers.SetResolver<T>((scope, ct) => ValueTask.FromResult<Validation<T>>(select(scope.Request)));

    public static TCollection As<TCollection, T, TBase>(
        this TCollection resolvers
        )
        where T : class
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => resolvers.SetResolver<T?>((context, ct) => context.ResolveOrDefault<TBase>(ct).MapAsync(b => b as T));

    public static TCollection RequestAs<TCollection, T>(
        this TCollection resolvers
        )
        where T : class
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => resolvers.SetResolver<T?>((scope, ct) => ValueTask.FromResult<Validation<T?>>(scope.Request as T));

    public static TCollection SetFixed<TCollection, T>(
        this TCollection dc,
        T fixedValue
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver<T>((scope, _) => ValueTask.FromResult<Validation<T>>(fixedValue));

    public static TCollection TrySetResolver<TCollection, T>(
        this TCollection dc,
        ResolveRequestDependency<T> resolve
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.GetResolver<T>() is not null
            ? dc
            : dc.SetResolver(resolve);

    private static ResolveRequestDependency<T?> MakeDefaultResolver<T>()
        => (scope, _) => ValueTask.FromResult<Validation<T?>>((T?)default);

    public static TCollection Configure<TCollection, T>(
        this TCollection resolvers,
        Func<ResolveRequestDependency<T?>, ResolveRequestDependency<T>> configure
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => resolvers.SetResolver(configure(resolvers.GetResolver<T?>() ?? MakeDefaultResolver<T?>()));

    public static TCollection ConfigureForRequestType<TCollection, TRequest, T>(
        this TCollection dc,
        Func<ResolveRequestDependency<T?>, ResolveRequestDependency<T>> configure
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.Configure<TCollection, T?>(next =>
        {
            ResolveRequestDependency<T?> configured = configure(next) as ResolveRequestDependency<T?>;
            return (scope, ct)
                => scope.Request is TRequest
                    ? configured(scope, ct)
                    : next(scope, ct);
        });

    public static TCollection ConfigureFor<TCollection, T>(
        this TCollection dc,
        ResolveRequestDependency<bool> predicate,
        Func<ResolveRequestDependency<T?>, ResolveRequestDependency<T>> configure
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.Configure<TCollection, T?>(next =>
        {
            ResolveRequestDependency<T?> configured = configure(next) as ResolveRequestDependency<T?>;
            return (scope, ct) => predicate(scope, ct).BindAsync(useConfigured => useConfigured
                ? configured(scope, ct)
                : next(scope, ct));
        });

    public static TCollection ConfigureAsNullCoalesce<TCollection, T>(
        this TCollection dc,
        ResolveRequestDependency<T> resolver
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.Configure<TCollection, T?>(next =>
        {
            ResolveRequestDependency<T?> configured = resolver as ResolveRequestDependency<T?>;
            return (scope, ct) => next(scope, ct).BindAsync(value => value is not null
                ? ValueTask.FromResult<Validation<T?>>(value)
                : configured(scope, ct));
        });

    public static TCollection ConfigureDefault<TCollection, T>(
        this TCollection dc,
        T defaultValue
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.ConfigureAsNullCoalesce((scope, _) => ValueTask.FromResult<Validation<T?>>(defaultValue));
}
