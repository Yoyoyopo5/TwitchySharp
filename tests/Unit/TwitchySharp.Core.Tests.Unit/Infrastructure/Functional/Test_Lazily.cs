using System.Diagnostics;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Tests.Unit.Toolkit;

namespace TwitchySharp.Core.Tests.Unit.Infrastructure.Functional;

public class Test_Lazily
{
    [Fact]
    public async Task Lazily_ConcurrentTasksWithSameKey_AllReturnSameResultInLazyTime()
    {
        const int WORKER_COUNT = 16;
        const int WORK_MS = 20;


        Func<int, CancellationToken, ValueTask<object>> concurrent = ThreadSafety.Lazily<int, int, object>(i => 0)(
            async (_, ct) =>
            {
                await Task.Delay(WORK_MS, ct);
                return new object();
            });

        Stopwatch sw = Stopwatch.StartNew();
        object[] results = await Concurrency.RunConcurrently(WORKER_COUNT, i => concurrent(i, TestContext.Current.CancellationToken).AsTask(), TestContext.Current.CancellationToken);
        sw.Stop();

        Assert.True(sw.ElapsedMilliseconds < WORKER_COUNT * WORK_MS);
        Assert.Single(results.ToHashSet());
    }

    [Fact]
    public async Task Lazily_SerialTasksWithSameKey_AllReturnUniqueResult()
    {
        const int WORKER_COUNT = 16;

        Func<int, CancellationToken, ValueTask<object>> concurrent = ThreadSafety.Lazily<int, int, object>(i => 0)(
            async (_, ct) => await Task.Run(async () => new object()));

        object[] result = await Enumerable.Range(0, WORKER_COUNT)
            .ToAsyncEnumerable()
            .Select(async (i, _, ct) => await concurrent(i, ct))
            .ToArrayAsync(TestContext.Current.CancellationToken);

        Assert.Equal(WORKER_COUNT, result.ToHashSet().Count);
    }

    [Fact]
    public async Task Lazily_ConcurrentTasksWithUniqueKeys_AllReturnUniqueResult()
    {
        const int WORKER_COUNT = 16;
        const int WORK_MS = 20;

        Func<int, CancellationToken, ValueTask<object>> concurrent = ThreadSafety.Lazily<int, int, object>(i => i)(
            async (_, ct) =>
            {
                await Task.Delay(WORK_MS, ct);
                return new object();
            });

        object[] results = await Concurrency.RunConcurrently(WORKER_COUNT, i => concurrent(i, default).AsTask(), TestContext.Current.CancellationToken);

        Assert.Equal(WORKER_COUNT, results.ToHashSet().Count);
    }

}
