using System.Collections.Immutable;
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
        => dc.SetResolver<T>((scope, ct) => ValueTask.FromResult(resolve(scope)));

    public static TCollection SetResolver<TCollection, T>(
        this TCollection dc,
        Func<ITwitchRequestDependencyScope, T> resolve
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver<T>((scope, ct) => ValueTask.FromResult<Validation<T>>(resolve(scope)));

    public static TCollection SetResolver<TCollection, T>(
        this TCollection dc,
        Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<T>> resolve
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver<T>(async (scope, ct) => await resolve(scope, ct));

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

    public static RequestDependencyConditionalConfiguration<TCollection> When<TCollection>(
        this TCollection dc,
        ResolveRequestDependency<bool> predicate
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => new(dc, predicate);

    public static RequestDependencyConditionalConfiguration<TCollection> When<TCollection>(
        this TCollection dc,
        Func<ITwitchRequestDependencyScope, bool> predicate
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => new(dc, (scope, ct) => ValueTask.FromResult<Validation<bool>>(predicate(scope)));

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

    public static TCollection ConfigureConditional<TCollection, T>(
        this TCollection dc,
        Func<T?, ITwitchRequestDependencyScope, CancellationToken, ValueTask<Validation<bool>>> predicate,
        Func<T?, ITwitchRequestDependencyScope, CancellationToken, ValueTask<Validation<T>>> conditionalResolve
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.Configure<TCollection, T?>(next => (scope, ct) =>
        {
            return next(scope, ct)
                .BindAsync(t => predicate(t, scope, ct)
                .BindAsync(async result => result
                    ? await conditionalResolve(t, scope, ct).MapAsync(t => (T?)t)
                    : t));
        });
}

// Want to be able to configure the same type that we use for the predicate

// .When((scope, ct) => scope.Resolve<HttpRequestMessage>(ct).IsBlockedRequest)
// .Configure<HttpResponseMessage?>(next => (scope, ct) => null)
// .EndWhen()

// .ConfigureConditional<HttpRequestMessage>(
// predicate: (request, scope, ct) => request.Host == "badhost.com",
// (request, scope, ct) =>
// {
//      request.Host = "goodhost.com";
//      return request;
// });

// Infinite loops:
// client.SetResolver<HttpRequestMessage>((scope, ct) => scope.ResolveOrDefault<HttpRequestMessage>())
// client.Configure<HttpRequestMessage>(next => (scope, ct) => scope.ResolveOrDefault<HttpRequestMessage>())
// client.When((scope, ct) => scope.ResolveOrDefault<HttpRequestMessage>() is GoodRequest)
//  .Configure<HttpRequestMessage>(next => )


public record RequestDependencyConditionalConfiguration<TCollection>(
    TCollection Collection,
    ResolveRequestDependency<bool> Predicate
    )
    : ITwitchRequestDependencyCollection<RequestDependencyConditionalConfiguration<TCollection>>
    where TCollection : ITwitchRequestDependencyCollection<TCollection>
{
    private ImmutableDictionary<Type, Delegate> ConditionalResolvers { get; init; }
        = ImmutableDictionary.Create<Type, Delegate>();
    private ImmutableDictionary<Type, Func<TCollection, TCollection>> ConfigurationApplicators { get; init; }
        = ImmutableDictionary.Create<Type, Func<TCollection, TCollection>>();

    public TCollection EndWhen()
        => ConfigurationApplicators.Values.Aggregate(Collection, (configured, nextApplicator) => nextApplicator(configured));

    public ResolveRequestDependency<T>? GetResolver<T>()
        => ConditionalResolvers.GetValueOrDefault(typeof(T)) as ResolveRequestDependency<T> ?? Collection.GetResolver<T>();
    public RequestDependencyConditionalConfiguration<TCollection> SetResolver<T>(ResolveRequestDependency<T> resolve)
        => this with
        {
            ConditionalResolvers = ConditionalResolvers.SetItem(typeof(T), resolve),
            ConfigurationApplicators = ConfigurationApplicators.SetIfKeyNotPresent(
                typeof(T),
                collection => collection.Configure<TCollection, T?>(next => (scope, ct) => Predicate(scope, ct).BindAsync(result => result
                    ? resolve.Map(t => t)(scope, ct)
                    : next(scope, ct)
                )))
        };
}

//       Makes Http Request
//              |
//          Conditional
//          /          \
//   True Branch      False Branch
//   calls next       calls next
//          \          /
//            Resolver
//               |
//          Conditional
//          /          \
//   True Branch      False Branch
//   calls next       calls next
//          \          /
//            Resolver
