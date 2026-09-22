using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

/// <summary>
/// Shorthand for <see cref="ITwitchRequestDependencyCollection{T}"/> where the generic type parameter is
/// <see cref="ITwitchRequestDependencyCollection"/>.
/// </summary>
/// <remarks>
/// This can implemented on types implementing <see cref="ITwitchRequestDependencyCollection{T}"/>
/// so that they can be assigned to <see cref="ITwitchRequestDependencyCollection"/>.
/// </remarks>
public interface ITwitchRequestDependencyCollection
    : ITwitchRequestDependencyCollection<ITwitchRequestDependencyCollection>;

/// <summary>
/// Represents a collection of dependency resolvers to be used when sending a <see cref="TwitchRequest"/>.
/// </summary>
/// <typeparam name="TSelf">
/// The type implementing the interface.
/// This is the self type to support fluent and immutable implementations.
/// </typeparam>
public interface ITwitchRequestDependencyCollection<out TSelf>
    where TSelf : ITwitchRequestDependencyCollection<TSelf>
{
    /// <summary>
    /// Sets the resolver for <typeparamref name="T"/>, overwriting the existing resolver if it has already been set.
    /// </summary>
    /// <typeparam name="T">The dependency type.</typeparam>
    /// <param name="resolve">The resolver to set.</param>
    /// <returns>This with the resolver set.</returns>
    TSelf SetResolver<T>(ResolveRequestDependency<T> resolve);
    /// <summary>
    /// Get the resolver for <typeparamref name="T"/>, or <see langword="null"/> if it has not been set.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="SetResolver{T}(ResolveRequestDependency{T})" path="/typeparam[@name = 'T']"/></typeparam>
    /// <returns>The resolver for <typeparamref name="T"/> or <see langword="null"/>.</returns>
    ResolveRequestDependency<T>? GetResolver<T>();
}

/// <summary>
/// Contains various extension methods for configurating request dependency collections.
/// </summary>
public static class ITwitchRequestDependencyCollectionExtensions
{
    /// <typeparam name="TCollection">The dependency collection type.</typeparam>
    /// <param name="dc">The dependency collection.</param>
    /// <returns>The dependency collection with the resolver set.</returns>
    /// <inheritdoc cref="ITwitchRequestDependencyCollection{T}.SetResolver{T}(ResolveRequestDependency{T})" />
    public static TCollection SetResolver<TCollection, T>(
        this TCollection dc,
        Func<ITwitchRequestDependencyScope, Validation<T>> resolve)
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver<T>((scope, ct) => ValueTask.FromResult(resolve(scope)));

    /// <inheritdoc cref="SetResolver{TCollection, T}(TCollection, Func{ITwitchRequestDependencyScope, Validation{T}})"/>
    public static TCollection SetResolver<TCollection, T>(
        this TCollection dc,
        Func<ITwitchRequestDependencyScope, T> resolve
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver<T>((scope, ct) => ValueTask.FromResult<Validation<T>>(resolve(scope)));

    /// <inheritdoc cref="SetResolver{TCollection, T}(TCollection, Func{ITwitchRequestDependencyScope, Validation{T}})"/>
    public static TCollection SetResolver<TCollection, T>(
        this TCollection dc,
        Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<T>> resolve
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver<T>(async (scope, ct) => await resolve(scope, ct));

    /// <summary>
    /// Set a resolver for <typeparamref name="T"/> that resolves its value from a resolved <typeparamref name="TFrom"/>
    /// using the provided <paramref name="select"/> function.
    /// </summary>
    /// <typeparam name="TFrom">The type of dependency to derive <typeparamref name="T"/> from.</typeparam>
    /// <param name="select">The selector function to use.</param>
    /// <inheritdoc cref="SetResolver{TCollection, T}(TCollection, Func{ITwitchRequestDependencyScope, Validation{T}})"/>
    public static TCollection From<TCollection, T, TFrom>(
        this TCollection dc,
        Func<TFrom?, T> select
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver<T>((context, ct) =>
            context.ResolveOrDefault<TFrom>(ct).MapAsync(f => select(f)));

    /// <summary>
    /// Map a <see cref="Nullable{TStruct}"/> to <typeparamref name="TStruct"/>,
    /// using the <paramref name="defaultValue"/> if <see langword="null"/>.
    /// </summary>
    internal static TCollection MapNullableStruct<TCollection, TStruct>(
        this TCollection dc,
        TStruct defaultValue = default
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        where TStruct : struct
        => dc.From<TCollection, TStruct, TStruct?>(nullable => nullable ?? defaultValue);

    /// <summary>
    /// Set a resolver for <typeparamref name="T"/> that resolves its value from the <see cref="ITwitchRequestDependencyScope.Request"/>
    /// using the provided <paramref name="select"/> function.
    /// </summary>
    /// <param name="select">The selector function to use.</param>
    /// <inheritdoc cref="SetResolver{TCollection, T}(TCollection, Func{ITwitchRequestDependencyScope, Validation{T}})"/>
    public static TCollection FromRequest<TCollection, T>(
        this TCollection dc,
        Func<TwitchRequest, T> select
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver<T>((scope, ct) => ValueTask.FromResult<Validation<T>>(select(scope.Request)));

    /// <summary>
    /// Set a resolver for <typeparamref name="T"/> that resolves its value by casting the resolved <typeparamref name="TBase"/> to <typeparamref name="T"/> using <see langword="as"/>.
    /// </summary>
    /// <typeparam name="TBase">The type of dependency to cast from.</typeparam>
    /// <inheritdoc cref="SetResolver{TCollection, T}(TCollection, Func{ITwitchRequestDependencyScope, Validation{T}})"/>
    public static TCollection As<TCollection, T, TBase>(
        this TCollection dc
        )
        where T : class
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver<T?>((context, ct) => context.ResolveOrDefault<TBase>(ct).MapAsync(b => b as T));

    /// <summary>
    /// Set a resolver for <typeparamref name="T"/> that resolves its value by casting <see cref="ITwitchRequestDependencyScope.Request"/> to <typeparamref name="T"/> using <see langword="as"/>.
    /// </summary>
    /// <inheritdoc cref="As{TCollection, T, TBase}(TCollection)"/>
    public static TCollection RequestAs<TCollection, T>(
        this TCollection dc
        )
        where T : class
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver<T?>((scope, ct) => ValueTask.FromResult<Validation<T?>>(scope.Request as T));

    /// <summary>
    /// Set a resolver for <typeparamref name="T"/> that always resolves to <paramref name="fixedValue"/>.
    /// </summary>
    /// <param name="fixedValue">The value to resolve for <typeparamref name="T"/>.</param>
    /// <inheritdoc cref="SetResolver{TCollection, T}(TCollection, Func{ITwitchRequestDependencyScope, Validation{T}})"/>
    public static TCollection SetFixed<TCollection, T>(
        this TCollection dc,
        T fixedValue
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver<T>((scope, _) => ValueTask.FromResult<Validation<T>>(fixedValue));

    /// <summary>
    /// Set the resolver for <typeparamref name="T"/> if a resolver for <typeparamref name="T"/> is not
    /// already present in the collection.
    /// </summary>
    /// <inheritdoc cref="SetResolver{TCollection, T}(TCollection, Func{ITwitchRequestDependencyScope, Validation{T}})"/>
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

    /// <summary>
    /// Set the resolver for <typeparamref name="T"/> to the output of <paramref name="configure"/>.
    /// </summary>
    /// <param name="configure">
    /// A function that receives the previously configured resolver for <typeparamref name="T"/> (or a default, <see langword="null"/> returning resolver if one is not present)
    /// and returns a new resolver for <typeparamref name="T"/> that will replace the existing resolver, if it exists.
    /// </param>
    /// <inheritdoc cref="SetResolver{TCollection, T}(TCollection, Func{ITwitchRequestDependencyScope, Validation{T}})"/>
    public static TCollection Configure<TCollection, T>(
        this TCollection dc,
        Func<ResolveRequestDependency<T?>, ResolveRequestDependency<T>> configure
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.SetResolver(configure(dc.GetResolver<T?>() ?? MakeDefaultResolver<T?>()));

    /// <summary>
    /// Entry point for configuring conditional resolver paths.
    /// </summary>
    /// <remarks>
    /// <b>WARNING:</b> resolving a dependency in the <paramref name="predicate"/> that is configured
    /// in the returned <see cref="RequestDependencyConditionalConfiguration{TCollection}"/> will cause
    /// a circular resolution path (i.e. an infinite loop). To apply conditional configuration to a
    /// same-typed dependency, use <see cref="ConfigureConditional"/>.
    /// </remarks>
    /// <param name="predicate">
    /// The predicate that, if returning <see langword="true"/>,
    /// determines if the resolvers configured on the returned <see cref="RequestDependencyConditionalConfiguration{TCollection}"/>
    /// are used.
    /// </param>
    /// <returns>A new dependency collection whose resolvers will only be used if <paramref name="predicate"/> returns <see langword="true"/> during resolution.</returns>
    /// <inheritdoc cref="SetResolver{TCollection, T}(TCollection, Func{ITwitchRequestDependencyScope, Validation{T}})"/>
    public static RequestDependencyConditionalConfiguration<TCollection> When<TCollection>(
        this TCollection dc,
        ResolveRequestDependency<bool> predicate
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => new(dc, predicate);

    /// <inheritdoc cref="When{TCollection}(TCollection, ResolveRequestDependency{bool})"/>
    public static RequestDependencyConditionalConfiguration<TCollection> When<TCollection>(
        this TCollection dc,
        Func<ITwitchRequestDependencyScope, bool> predicate
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => new(dc, (scope, ct) => ValueTask.FromResult<Validation<bool>>(predicate(scope)));

    /// <summary>
    /// Applies a resolver for <typeparamref name="T"/> after the previously configured resolver,
    /// so that <paramref name="resolve"/> is only evaluated if the previous resolver returns a <see langword="null"/> <typeparamref name="T"/>.
    /// </summary>
    /// <inheritdoc cref="SetResolver{TCollection, T}(TCollection, Func{ITwitchRequestDependencyScope, Validation{T}})"/>
    public static TCollection ConfigureAsNullCoalesce<TCollection, T>(
        this TCollection dc,
        ResolveRequestDependency<T> resolve
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.Configure<TCollection, T?>(next =>
        {
            ResolveRequestDependency<T?> configured = resolve as ResolveRequestDependency<T?>;
            return (scope, ct) => next(scope, ct).BindAsync(value => value is not null
                ? ValueTask.FromResult<Validation<T?>>(value)
                : configured(scope, ct));
        });


    /// <summary>
    /// Set a default value for <typeparamref name="T"/> that is returned if all previous resolvers for <typeparamref name="T"/> return <see langword="null"/>.
    /// </summary>
    /// <param name="defaultValue">The default value.</param>
    /// <inheritdoc cref="SetResolver{TCollection, T}(TCollection, Func{ITwitchRequestDependencyScope, Validation{T}})"/>
    public static TCollection ConfigureDefault<TCollection, T>(
        this TCollection dc,
        T defaultValue
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.ConfigureAsNullCoalesce((scope, _) => ValueTask.FromResult<Validation<T?>>(defaultValue));

    /// <summary>
    /// Apply a resolver for <typeparamref name="T"/> conditionally.
    /// </summary>
    /// <remarks>
    /// This can be used to apply a function to <typeparamref name="T"/> where the predicate also depends on
    /// <typeparamref name="T"/> without creating an infinite loop. <typeparamref name="T"/> is resolved from
    /// the previously configured resolver before being passed to <paramref name="predicate"/> and
    /// <paramref name="conditionalResolve"/>. For conditional configuration with predicates that do not depend
    /// on the resolver type(s) being configured, use <see cref="When"/>.
    /// </remarks>
    /// <param name="predicate">
    /// The predicate that, if returning <see langword="true"/>,
    /// determines if <paramref name="conditionalResolve"/> is evaluated.
    /// If returning <see langword="false"/>, the previously resolved <typeparamref name="T"/> is used.
    /// </param>
    /// <param name="conditionalResolve">The resolver that should be conditionally evaluated.</param>
    /// <inheritdoc cref="SetResolver{TCollection, T}(TCollection, Func{ITwitchRequestDependencyScope, Validation{T}})"/>
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

/// <summary>
/// Applies conditional resolver configuration to an existing <see cref="ITwitchRequestDependencyCollection"/>.
/// </summary>
/// <typeparam name="TCollection">The collection type to apply conditional configuration to.</typeparam>
/// <param name="Collection">The collection to apply conditional configuration to.</param>
/// <param name="Predicate">The resolve-time condition that must be <see langword="true"/> for configuration to apply.</param>
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

    /// <summary>
    /// Apply the conditional configuration to <see cref="Collection"/>.
    /// </summary>
    /// <returns>The <see cref="Collection"/> with each resolver set on this instance configured conditionally against <see cref="Predicate"/>.</returns>
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
