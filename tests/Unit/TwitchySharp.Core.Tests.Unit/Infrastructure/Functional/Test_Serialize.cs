using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Core.Tests.Unit.Infrastructure.Functional;

public class Test_Serialize
{
    private async Task<T[]> RunConcurrently<T>(
        int workerCount,
        ManualResetEventSlim gate,
        Func<int, Task<T>> func,
        CancellationToken ct
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

        int[] results = await RunConcurrently(WORKER_COUNT, gate, i => concurrentFunc(i, TestContext.Current.CancellationToken).AsTask(), TestContext.Current.CancellationToken);

        Assert.Equal(WORKER_COUNT * WORKER_ITERATIONS, state);
        Assert.Equal(Enumerable.Range(1, WORKER_COUNT).Select(w => w * WORKER_ITERATIONS), results);
    }

    [Fact]
    public async Task Serialize_ConcurrentTaskWithDefaultProviderAndUniqueKeys_CompleteInParallel()
    {

    }

    [Fact]
    public async Task Serialize_WithLockProvider_UsesLockProvider()
    {

    }
}
