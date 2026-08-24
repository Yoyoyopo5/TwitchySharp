using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

internal delegate ValueTask<DependencyResult<object>> ResolveRequestDependency(
    RequestDependencyScope scope,
    CancellationToken ct);

/// <summary>
/// A function that resolves a specific Twitch API request dependency of <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of dependency the function resolves.</typeparam>
/// <param name="scope">The dependency scope for the request.</param>
/// <param name="ct">Cancellation token</param>
/// <returns>A <see cref="ValueTask"/> containing the resolved dependency result.</returns>
public delegate ValueTask<DependencyResult<T>> ResolveRequestDependency<T>(
    RequestDependencyScope scope,
    CancellationToken ct);

/// <summary>
/// A result for a resolved Twitch API request dependency.
/// </summary>
/// <typeparam name="T">The type of dependency the result is for.</typeparam>
/// <param name="Value">The result's value, if any.</param>
/// <param name="UpdatedScope">The new request dependency scope after resolving <paramref name="Value"/>.</param>
/// <param name="Error">The error that occurred during resolution, if any.</param>
public record DependencyResult<T>(
    T? Value,
    RequestDependencyScope UpdatedScope,
    Error? Error
    )
{
    /// <summary>
    /// Create a result with no error.
    /// </summary>
    /// <param name="value"><inheritdoc cref="DependencyResult{T}" path="/param[@name='Value']"/></param>
    /// <param name="updatedScope"><inheritdoc cref="DependencyResult{T}" path="/param[@name='UpdatedScope']"/></param>
    public DependencyResult(T? value, RequestDependencyScope updatedScope)
        : this(value, updatedScope, null) { }

    /// <summary>
    /// Create an error result.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <param name="updatedScope">The new request dependency scope after resolving the error.</param>
    public DependencyResult(Error error, RequestDependencyScope updatedScope)
        : this(default, updatedScope, error) { }

    /// <summary>
    /// Resolve the request dependency of <typeparamref name="TNext"/>
    /// using the <see cref="UpdatedScope"/> of the <see cref="DependencyResult{T}"/>.
    /// </summary>
    /// <remarks>
    /// If the <see cref="DependencyResult{T}.Error"/> is not <see langword="null"/>,
    /// <typeparamref name="TNext"/> resolution is skipped and the error is passed to the new result.
    /// </remarks>
    /// <typeparam name="TNext">The next type to resolve.</typeparam>
    /// <param name="ct">Cancellation token</param>
    /// <returns>A <see cref="ValueTask"/> containing a <see cref="DependencyResult{T}"/>.</returns>
    public async ValueTask<DependencyResult<TNext>> GetOrDefault<TNext>(CancellationToken ct)
        => Error is not null
            ? new(default, UpdatedScope, Error)
            : await UpdatedScope.GetOrDefault<TNext>(ct);

    /// <summary>
    /// Create a <see cref="DependencyResult{T}"/> from a <see cref="Validation"/> and <see cref="RequestDependencyScope"/>,
    /// passing the <see cref="Validation"/> value or error, if any, to the returned result.
    /// </summary>
    /// <param name="validation">The <see cref="Validation"/> to construct the result from.</param>
    /// <param name="scope">The <see cref="RequestDependencyScope"/> to use in the result.</param>
    /// <returns>A new <see cref="DependencyResult{T}"/> with the <see cref="Validation"/> value or error.</returns>
    internal static DependencyResult<T> From(Validation<T?> validation, RequestDependencyScope scope)
        => validation.Match(
            scope.ToResult<T>,
            scope.ToResult
            );
}

/// <summary>
/// Provides dependency resolvers scoped to a specific <see cref="TwitchRequest"/>.
/// </summary>
public abstract record RequestDependencyScope
{
    /// <summary>
    /// The <see cref="TwitchRequest"/> associated with scope.
    /// </summary>
    public required TwitchRequest Request { get; init; }
    /// <summary>
    /// Resolve the dependency of <typeparamref name="T"/> for the current request scope.
    /// </summary>
    /// <typeparam name="T">The type of dependency to resolve.</typeparam>
    /// <param name="ct">Cancellation token</param>
    /// <returns>A <see cref="ValueTask"/> containing the resolver result.</returns>
    public abstract ValueTask<DependencyResult<T>> GetOrDefault<T>(CancellationToken ct);
}

internal static class RequestDependencyScopeExtensions
{
    public static DependencyResult<T> ToResult<T>(
        this RequestDependencyScope nextScope,
        T? validResult
        )
        => new(validResult, nextScope);

    public static DependencyResult<T> ToResult<T>(
        this RequestDependencyScope nextScope,
        Error error
        )
        => new(error, nextScope);
}

internal static class DependencyResolverCollectionExtensions
{
    public static ResolveRequestDependency<T>? GetResolver<T>(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers
        )
        => resolvers.GetValueOrDefault(typeof(T)) is not ResolveRequestDependency resolver
            ? null
            : async (context, ct) =>
            {
                (object? resolved, RequestDependencyScope nextContext, Error? error)
                    = await resolver(context, ct);

                try
                {
                    return error is not null
                        ? new DependencyResult<T>(default, nextContext, error)
                        : new DependencyResult<T>((T?)resolved, nextContext, error);
                }
                catch (InvalidCastException ex)
                {
                    return nextContext.ToResult<T>(new ExceptionError(ex));
                }
            };

    internal static ResolveRequestDependency? GetResolver(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers,
        Type ofType
        )
        => resolvers.GetValueOrDefault(ofType);

    public static ImmutableDictionary<Type, ResolveRequestDependency> SetResolver<T>(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers,
        ResolveRequestDependency<T> resolve
        )
        => resolvers.SetItem(typeof(T), async (context, ct) =>
        {
            (T? resolved, RequestDependencyScope nextContext, Error? error)
                = await resolve(context, ct);
            return new(resolved, nextContext, error);
        });

    internal static ImmutableDictionary<Type, ResolveRequestDependency> SetResolver(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers,
        Type forType,
        ResolveRequestDependency resolve
        )
        => resolvers.SetItem(forType, resolve);

    public static ImmutableDictionary<Type, ResolveRequestDependency> TrySetResolver<T>(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers,
        ResolveRequestDependency<T> resolve
        )
        => resolvers.ContainsKey(typeof(T))
            ? resolvers
            : resolvers.SetResolver(resolve);

    public static ImmutableDictionary<Type, ResolveRequestDependency> Configure<T>(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers,
        Func<ResolveRequestDependency<T>, ResolveRequestDependency<T>> configure
        )
        => resolvers.SetResolver(configure(resolvers.GetResolver<T>() ?? ((context, ct) => ValueTask.FromResult(context.ToResult((T?)default)))));

    internal static ImmutableDictionary<Type, ResolveRequestDependency> Configure(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers,
        Type forType,
        Func<ResolveRequestDependency, ResolveRequestDependency> configure
        )
        => resolvers.SetResolver(forType, configure(resolvers.GetValueOrDefault(forType) ?? ((context, ct) => ValueTask.FromResult(context.ToResult((object?)null)))));

    public static ImmutableDictionary<Type, ResolveRequestDependency> ConfigureFor<TRequest, T>(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers,
        Func<ResolveRequestDependency<T>, ResolveRequestDependency<T>> configure
        )
        => resolvers.Configure<T>(next =>
        {
            ResolveRequestDependency<T> configured = configure(next);
            return (context, ct)
                => context.Request is TRequest
                    ? configured(context, ct)
                    : next(context, ct);
        });

    internal static ImmutableDictionary<Type, ResolveRequestDependency> ConfigureFor<TRequest>(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers,
        Type forType,
        Func<ResolveRequestDependency, ResolveRequestDependency> configure
        )
        => resolvers.Configure(forType, next =>
        {
            ResolveRequestDependency configured = configure(next);
            return (context, ct)
                => context.Request is TRequest
                    ? configured(context, ct)
                    : next(context, ct);
        });
}

internal sealed record MemoizingRequestDependencyScope
    : RequestDependencyScope, IDisposable
{
    public ImmutableDictionary<Type, Validation<object?>> Memos { get; init; }
        = ImmutableDictionary<Type, Validation<object?>>.Empty;
    public ImmutableDictionary<Type, ResolveRequestDependency> Resolvers { get; init; }
        = ImmutableDictionary<Type, ResolveRequestDependency>.Empty;
    public override async ValueTask<DependencyResult<T>> GetOrDefault<T>(CancellationToken ct)
    {
        if (Memos.TryGetValue(typeof(T), out Validation<object?> memo))
            return DependencyResult<T>.From(memo.Map(obj => (T?)obj), this);

        if (Resolvers.GetResolver<T>() is not ResolveRequestDependency<T> resolver)
            return new DependencyResult<T>(default, this, null);

        (T? value, RequestDependencyScope nextContext, Error? error)
            = await resolver(this, ct);

        nextContext = nextContext is MemoizingRequestDependencyScope memoizingContext
            ? memoizingContext with { Memos = Memos.SetItem(typeof(T), value) }
            : nextContext;

        return new DependencyResult<T>(value, nextContext, error);
    }
    public void Dispose()
    {
        foreach (Validation<object?> memo in Memos)
        {
            memo.Match(e => false, val =>
            {
                if (val is IDisposable disposable)
                    disposable.Dispose();
                return true;
            });
        }
    }
}
