using TwitchySharp.Infrastructure.Functional;
using Xunit;

namespace TwitchySharp.Tests.Unit.Toolkit;

public static class Concurrency
{
    public class Probe
    {
        private int _max = 0;
        private int _count = 0;
        public int MaxConcurrency => _max;
        public IDisposable Enter()
        {
            int current = Interlocked.Increment(ref _count);
            int max = Volatile.Read(ref _max);
            if (current > max)
                Interlocked.CompareExchange(ref _max, current, max);
            return new FunctionalDisposable(() => Interlocked.Decrement(ref _count));
        }

        public void AssertSerialExecution()
        {
            Assert.Equal(1, _max);
        }

        public void AssertParallelExecution()
        {
            Assert.NotEqual(1, _max);
            Assert.True(_max > 1);
        }
    }

    public static async Task<T[]> RunConcurrently<T>(
        int workerCount,
        Func<int, Task<T>> func,
        CancellationToken ct
        )
    {
        ManualResetEventSlim gate = new(false);
        Task<T>[] tasks = Enumerable.Range(0, workerCount).Select(i => Task.Run(() =>
        {
            gate.Wait(ct);
            return func(i);
        })).ToArray();
        gate.Set();
        return await Task.WhenAll(tasks);
    }

    public static async Task RunConcurrently(
        int workerCount,
        Func<int, Task> func,
        CancellationToken ct
        )
    {
        ManualResetEventSlim gate = new(false);
        Task[] tasks = Enumerable.Range(0, workerCount).Select(i => Task.Run(() =>
        {
            gate.Wait(ct);
            return func(i);
        })).ToArray();
        gate.Set();
        await Task.WhenAll(tasks);
    }
}
