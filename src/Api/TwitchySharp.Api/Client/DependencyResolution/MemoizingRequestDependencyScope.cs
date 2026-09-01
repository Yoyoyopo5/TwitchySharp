using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

internal class MemoizingRequestDependencyScope(
    TwitchRequest request,
    ITwitchRequestDependencyCollection dependencyCollection
    )
    : ITwitchRequestDependencyScope, IDisposable
{
    public TwitchRequest Request { get; } = request;
    private readonly Dictionary<Type, Validation<object?>> _memos = [];
    public ITwitchRequestDependencyCollection DependencyCollection { get; private set; } = dependencyCollection;

    public MemoizingRequestDependencyScope SetResolver<T>(ResolveRequestDependency<T> resolve)
    {
        DependencyCollection = DependencyCollection.SetResolver<T>(resolve);
        _memos.Remove(typeof(T));
        return this;
    }
    ITwitchRequestDependencyScope ITwitchRequestDependencyCollection<ITwitchRequestDependencyScope>.SetResolver<T>(ResolveRequestDependency<T> resolve)
        => SetResolver(resolve);
    ITwitchRequestDependencyCollection ITwitchRequestDependencyCollection<ITwitchRequestDependencyCollection>.SetResolver<T>(ResolveRequestDependency<T> resolve)
        => SetResolver(resolve);
    public ResolveRequestDependency<T>? GetResolver<T>() => GetResolver<T>();

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
