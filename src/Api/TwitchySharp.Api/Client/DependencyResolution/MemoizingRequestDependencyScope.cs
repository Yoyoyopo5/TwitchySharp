using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

internal record DependencyDisposalDictionary
{
    public ImmutableDictionary<Type, Action<object>> Disposers { get; init; }
        = ImmutableDictionary.Create<Type, Action<object>>();

    public DependencyDisposalDictionary Add<T>(Action<T> dispose)
    {
        void boxedDispose(object o) { if (o is T t) dispose(t); }
        return this with { Disposers = Disposers.Add(typeof(T), boxedDispose) };
    }

    public Action<object>? GetOrDefault(Type dependencyType)
        => Disposers.GetValueOrDefault(dependencyType);
}

/// <summary>
/// Extensions relating to dependency lifetimes and disposal.
/// </summary>
public static class ITwitchRequestDependencyCollectionDisposeExtensions
{
    public static TCollection ConfigureDispose<TCollection, T>(
        this TCollection dc,
        Action<T> dispose
        )
        where TCollection : ITwitchRequestDependencyCollection<TCollection>
        => dc.Configure<TCollection, DependencyDisposalDictionary>(next => (scope, ct) =>
            next(scope, ct).MapAsync(d =>
                d is not null
                ? d.Add(dispose)
                : new DependencyDisposalDictionary().Add(dispose)));
}

/// <summary>
/// A <see cref="ITwitchRequestDependencyScope"/> implementation that
/// memoizes resolved values, and disposes those values upon scope disposal.
/// </summary>
/// <param name="request"></param>
/// <param name="dependencyCollection"></param>
internal class MemoizingRequestDependencyScope(
    TwitchRequest request,
    ITwitchRequestDependencyCollection dependencyCollection
    )
    : ITwitchRequestDependencyScope, IAsyncDisposable
{
    public TwitchRequest Request { get; } = request;
    private readonly Dictionary<Type, Validation<object?>> _memos = [];
    public IReadOnlyDictionary<Type, Validation<object?>> Memos => _memos;

    /// <summary>
    /// The underlying <see cref="ITwitchRequestDependencyCollection"/> to resolve dependency values from.
    /// </summary>
    public ITwitchRequestDependencyCollection DependencyCollection { get; private set; } = dependencyCollection;

    /// <summary>
    /// Sets the resolver for <typeparamref name="T"/>, invalidating the existing memo if it exists.
    /// </summary>
    /// <typeparam name="T">The type to set the resolver for.</typeparam>
    /// <param name="resolve">The resolver function.</param>
    /// <returns><see langword="this"/> with the new resolver for <typeparamref name="T"/>.</returns>
    public MemoizingRequestDependencyScope SetResolver<T>(ResolveRequestDependency<T> resolve)
    {
        DependencyCollection = DependencyCollection.SetResolver<T>(resolve);
        InvalidateMemo<T>();
        return this;
    }
    ITwitchRequestDependencyScope ITwitchRequestDependencyCollection<ITwitchRequestDependencyScope>.SetResolver<T>(ResolveRequestDependency<T> resolve)
        => SetResolver(resolve);
    ITwitchRequestDependencyCollection ITwitchRequestDependencyCollection<ITwitchRequestDependencyCollection>.SetResolver<T>(ResolveRequestDependency<T> resolve)
        => SetResolver(resolve);
    public ResolveRequestDependency<T>? GetResolver<T>() => DependencyCollection.GetResolver<T>();

    private void InvalidateMemo<T>()
    {
        if (_memos.Remove(typeof(T), out Validation<object?> memo))
            memo.Map(memo =>
            {
                if (memo is IDisposable d)
                    d.Dispose();
                return memo;
            });
    }

    public ValueTask<Validation<T?>> ResolveOrDefault<T>(CancellationToken ct)
        => _memos.TryGetValue(typeof(T), out Validation<object?> memo)
            ? ValueTask.FromResult(memo.Map(obj => (T?)obj))
            : DependencyCollection.GetResolver<T>() is not ResolveRequestDependency<T> resolver
            ? ValueTask.FromResult<Validation<T?>>((T?)default)
            : resolver(this, ct).MapAsync(
            value =>
            {
                _memos.Add(typeof(T), value); // side-effect
                return (T?)value;
            });

    public async ValueTask DisposeAsync()
    {
        await ResolveOrDefault<DependencyDisposalDictionary>(default)
            .MapAsync(async d =>
            {
                if (d is null)
                    return d;

                foreach ((Type t, Validation<object?> v) in _memos)
                    if (d.GetOrDefault(t) is Action<object> dispose)
                        v.Map(memo =>
                        {
                            if (memo is not null)
                                dispose(memo);
                            return memo;
                        });

                return d;
            });
    }
}
