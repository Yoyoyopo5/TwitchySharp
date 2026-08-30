using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

public interface ITwitchRequestDependencyCollection
{
    ITwitchRequestDependencyCollection SetResolver<T>(ResolveRequestDependency<T> resolve);
    ResolveRequestDependency<T>? GetResolver<T>();
}

public static class ITwitchRequestDependencyCollectionExtensions
{
    public static ITwitchRequestDependencyCollection From<T, TFrom>(
        this ITwitchRequestDependencyCollection resolvers,
        Func<TFrom?, T?> select
        )
        => resolvers.SetResolver<T>((context, ct) =>
            context.GetOrDefault<TFrom>(ct).MapAsync(f => select(f)));

    public static ITwitchRequestDependencyCollection As<T, TBase>(
        this ITwitchRequestDependencyCollection resolvers
        )
        where T : class
        => resolvers.SetResolver<T>((context, ct) => context.GetOrDefault<TBase>(ct).MapAsync(b => b as T));

    public static ITwitchRequestDependencyCollection SetFixed<T>(
        this ITwitchRequestDependencyCollection dc,
        T fixedValue
        )
        => dc.SetResolver((scope, _) => ValueTask.FromResult(scope.ToResult(fixedValue)));

    public static ITwitchRequestDependencyCollection TrySetResolver<T>(
        this ITwitchRequestDependencyCollection dc,
        ResolveRequestDependency<T> resolve
        )
        => dc.GetResolver<T>() is not null
            ? dc
            : dc.SetResolver(resolve);

    private static ResolveRequestDependency<T> MakeDefaultResolver<T>()
        => (scope, _) => ValueTask.FromResult(scope.ToResult((T?)default));

    public static ITwitchRequestDependencyCollection Configure<T>(
        this ITwitchRequestDependencyCollection resolvers,
        Func<ResolveRequestDependency<T>, ResolveRequestDependency<T>> configure
        )
        => resolvers.SetResolver(configure(resolvers.GetResolver<T>() ?? MakeDefaultResolver<T>()));

    public static ITwitchRequestDependencyCollection ConfigureForRequestType<TRequest, T>(
        this ITwitchRequestDependencyCollection dc,
        Func<ResolveRequestDependency<T>, ResolveRequestDependency<T>> configure
        )
        => dc.Configure<T>(next =>
        {
            ResolveRequestDependency<T> configured = configure(next);
            return (context, ct)
                => context.Request is TRequest
                    ? configured(context, ct)
                    : next(context, ct);
        });

    public static ITwitchRequestDependencyCollection ConfigureFor<T>(
        this ITwitchRequestDependencyCollection dc,
        ResolveRequestDependency<bool> predicate,
        Func<ResolveRequestDependency<T>, ResolveRequestDependency<T>> configure
        )
        => dc.Configure<T>(next =>
        {
            ResolveRequestDependency<T> configured = configure(next);
            return async (context, ct) =>
            {
                (bool result, ITwitchRequestDependencyScope nextContext, Error? error)
                    = await predicate(context, ct);

                return error is not null
                    ? nextContext.ToResult<T>(error)
                    : result
                    ? await configured(nextContext, ct)
                    : await next(nextContext, ct);
            };
        });

    public static ITwitchRequestDependencyCollection ConfigureAsNullCoalesce<T>(
        this ITwitchRequestDependencyCollection dc,
        ResolveRequestDependency<T> resolver
        )
        => dc.Configure<T>(next => async (context, ct) =>
            await next(context, ct) switch
            {
                { Error: Error error } errored => new RequestDependencyResult<T>(error, errored.UpdatedScope),
                { Value: T value } resolved => new RequestDependencyResult<T>(value, resolved.UpdatedScope),
                { } none => await resolver(none.UpdatedScope, ct)
            });

    public static ITwitchRequestDependencyCollection ConfigureDefault<T>(
        this ITwitchRequestDependencyCollection dc,
        T defaultValue
        )
        => dc.ConfigureAsNullCoalesce((scope, _) => ValueTask.FromResult(scope.ToResult(defaultValue)));
}
