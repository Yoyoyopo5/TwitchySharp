using System.Diagnostics;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Core.Tests.Unit.Infrastructure.Functional;

public class Test_Serialize
{
    private static async Task<T[]> RunConcurrently<T>(
        int workerCount,
        ManualResetEventSlim gate,
        Func<int, Task<T>> func
    )
    {
        Task<T>[] tasks = Enumerable.Range(0, workerCount).Select(func).ToArray();
        gate.Set();
        return await Task.WhenAll(tasks);
    }

    [Fact]
    public async Task Serialize_ConcurrentTasksWithDefaultProviderAndSameKey_CompleteInSeries()
    {
        const int WORKER_COUNT = 512;
        const int WORKER_ITERATIONS = 16;

        int state = 0;
        ManualResetEventSlim gate = new(false);

        Func<int, CancellationToken, ValueTask<int>> concurrentFunc = ThreadSafety.Concurrently<int, string, int>(_ => "STATIC")(async (_, ct) =>
            await Task.Run(() =>
            {
                gate.Wait(ct);
                for (int i = 0; i < WORKER_ITERATIONS; i++)
                {
                    state++;
                }
                return state;
            }, ct)
        );

        int[] results = await RunConcurrently(WORKER_COUNT, gate, i => concurrentFunc(i, TestContext.Current.CancellationToken).AsTask());

        Assert.Equal(WORKER_COUNT * WORKER_ITERATIONS, state);
        Assert.Equal(Enumerable.Range(1, WORKER_COUNT).Select(w => w * WORKER_ITERATIONS), results);
    }

    [Fact]
    public async Task Serialize_ConcurrentTaskWithDefaultProviderAndUniqueKeys_CompleteInParallel()
    {
        const int WORK_MS = 10;
        const int WORKER_COUNT = 32;

        ManualResetEventSlim gate = new(false);

        Func<int, CancellationToken, ValueTask<int>> concurrent = ThreadSafety.Concurrently<int, int, int>(i => i)(
            async (_, ct) => await Task.Run(async () =>
            {
                gate.Wait(ct);
                await Task.Delay(WORK_MS, ct);
                return 0;
            }));

        Stopwatch sw = Stopwatch.StartNew();
        int[] result = await RunConcurrently(WORKER_COUNT, gate, i => concurrent(i, TestContext.Current.CancellationToken).AsTask());
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

        Func<int, CancellationToken, ValueTask<int>> concurrent = ThreadSafety.Concurrently<int, int, int>(i => 0, ProvideLock)(
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

        Func<int, CancellationToken, ValueTask<int>> concurrent = ThreadSafety.Concurrently<int, int, int>(i => 0, ProvideLock)(
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
