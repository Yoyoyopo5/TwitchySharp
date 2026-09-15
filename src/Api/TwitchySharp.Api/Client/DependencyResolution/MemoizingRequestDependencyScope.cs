using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

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
    : ITwitchRequestDependencyScope, IDisposable
{
    public TwitchRequest Request { get; } = request;
    private readonly Dictionary<Type, Validation<object?>> _memos = [];
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

    public void Dispose()
    {
        foreach (Validation<object?> memo in _memos.Values)
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
