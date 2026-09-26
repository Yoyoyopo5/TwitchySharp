using System;
using System.Threading.Tasks;

namespace TwitchySharp.Infrastructure.Functional;

internal record FunctionalDisposable(Action OnDispose) : IDisposable
{
    public void Dispose() => OnDispose();
}

internal record FunctionalAsyncDisposable(Func<ValueTask> OnDispose) : IAsyncDisposable
{
    public ValueTask DisposeAsync() => OnDispose();
}
