namespace TwitchySharp.Api.Tests.Unit.Client.RateLimiting;

public class Test_AwaitUsing
{
    private class StubDisposable : IAsyncDisposable
    {
        public bool Disposed { get; private set; }
        public ValueTask DisposeAsync()
        {
            Disposed = true;
            return ValueTask.CompletedTask;
        }
    }

    [Fact]
    public async Task AwaitUsing_FunctionCalledWithLiveDisposable_ThenDisposableDisposed()
    {
        StubDisposable stubDisposable = new();

        Func<ValueTask<bool>> mockFunction = () => ValueTask.FromResult(stubDisposable.Disposed);

        bool result = await ValueTask.FromResult<IAsyncDisposable>(stubDisposable).AwaitUsing(mockFunction);
        Assert.False(result);
        Assert.True(stubDisposable.Disposed);
    }
}
