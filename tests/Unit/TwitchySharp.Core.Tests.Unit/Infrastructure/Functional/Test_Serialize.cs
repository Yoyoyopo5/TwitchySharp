using System.Diagnostics;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Tests.Unit.Toolkit;

namespace TwitchySharp.Core.Tests.Unit.Infrastructure.Functional;

public class Test_Serialize
{
    [Fact]
    public async Task Serialize_ConcurrentTasksWithDefaultProviderAndSameKey_CompleteInSeries()
    {
        const int WORKER_COUNT = 512;
        const int WORKER_ITERATIONS = 16;

        int state = 0;

        Func<int, CancellationToken, ValueTask<int>> concurrentFunc = ThreadSafety.Serialize<int, string, int>(_ => "STATIC")(
            (_, ct) =>
            {
                for (int i = 0; i < WORKER_ITERATIONS; i++)
                {
                    state++;
                }
                return ValueTask.FromResult(state);
            });

        int[] results = await Concurrency.RunConcurrently(WORKER_COUNT, i => concurrentFunc(i, default).AsTask(), TestContext.Current.CancellationToken);

        Assert.Equal(WORKER_COUNT * WORKER_ITERATIONS, state);
    }

    [Fact]
    public async Task Serialize_ConcurrentTaskWithDefaultProviderAndUniqueKeys_CompleteInParallel()
    {
        const int WORK_MS = 10;
        const int WORKER_COUNT = 32;

        Func<int, CancellationToken, ValueTask<int>> concurrent = ThreadSafety.Serialize<int, int, int>(i => i)(
            async (_, ct) =>
            {
                await Task.Delay(WORK_MS, ct);
                return 0;
            });

        CancellationToken ct = TestContext.Current.CancellationToken;

        Stopwatch sw = Stopwatch.StartNew();
        int[] result = await Concurrency.RunConcurrently(WORKER_COUNT, i => concurrent(i, ct).AsTask(), ct);
        sw.Stop();

        Assert.True(sw.ElapsedMilliseconds < WORK_MS * WORKER_COUNT); // Not entirely deterministic, but should suffice in most conditions.
    }

    [Fact]
    public async Task Serialize_WithLockProvider_UsesLockProvider()
    {
        int lockCount = 0;

        ValueTask<IAsyncDisposable> ProvideLock(int key, CancellationToken ct)
        {
            Interlocked.Increment(ref lockCount);
            return ValueTask.FromResult<IAsyncDisposable>(new StubLock());
        }

        Func<int, CancellationToken, ValueTask<int>> concurrent = ThreadSafety.Serialize<int, int, int>(i => 0, ProvideLock)(
            async (_, ct) => await Task.Run(() => Task.FromResult(0)));

        int result = await concurrent(0, TestContext.Current.CancellationToken);

        Assert.Equal(1, lockCount);
    }

    [Fact]
    public async Task Serialize_WithLockProvider_DisposesLock()
    {
        StubLock @lock = new();

        ValueTask<IAsyncDisposable> ProvideLock(int key, CancellationToken ct)
            => ValueTask.FromResult<IAsyncDisposable>(@lock);

        Func<int, CancellationToken, ValueTask<int>> concurrent = ThreadSafety.Serialize<int, int, int>(i => 0, ProvideLock)(
            async (_, ct) => await Task.Run(() => Task.FromResult(0)));

        int result = await concurrent(0, TestContext.Current.CancellationToken);

        Assert.Equal(1, @lock.DisposeCount);
    }

    private class StubLock : IAsyncDisposable
    {
        public int DisposeCount => _disposeCount;
        private int _disposeCount = 0;
        public ValueTask DisposeAsync()
        {
            Interlocked.Increment(ref _disposeCount);
            return ValueTask.CompletedTask;
        }
    }
}
