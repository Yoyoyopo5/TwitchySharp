using System.Collections.Immutable;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api;

internal sealed record MemoizingRequestDependencyScope
    : ITwitchRequestDependencyScope, IDisposable
{
    public required TwitchRequest Request { get; init; }
    public ImmutableDictionary<Type, Validation<object?>> Memos { get; init; }
        = ImmutableDictionary<Type, Validation<object?>>.Empty;
    public required ITwitchRequestDependencyCollection DependencyCollection { get; init; }

    public MemoizingRequestDependencyScope SetResolver<T>(ResolveRequestDependency<T> resolve)
        => this with
        {
            DependencyCollection = DependencyCollection.SetResolver<T>(resolve),
            Memos = Memos.Remove(typeof(T))
        };
    ITwitchRequestDependencyCollection ITwitchRequestDependencyCollection.SetResolver<T>(ResolveRequestDependency<T> resolve)
        => SetResolver(resolve);
    ResolveRequestDependency<T>? ITwitchRequestDependencyCollection.GetResolver<T>()
        => DependencyCollection.GetResolver<T>();

    public async ValueTask<RequestDependencyResult<T>> GetOrDefault<T>(CancellationToken ct)
    {
        if (Memos.TryGetValue(typeof(T), out Validation<object?> memo))
            return RequestDependencyResult<T>.From(memo.Map(obj => (T?)obj), this);

        if (DependencyCollection.GetResolver<T>() is not ResolveRequestDependency<T> resolver)
            return new RequestDependencyResult<T>(default, this, null);

        (T? value, ITwitchRequestDependencyScope nextContext, Error? error)
            = await resolver(this, ct);

        nextContext = nextContext is MemoizingRequestDependencyScope memoizingContext
            ? memoizingContext with { Memos = Memos.SetItem(typeof(T), value) }
            : nextContext;

        return new RequestDependencyResult<T>(value, nextContext, error);
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
