namespace TwitchySharp.Tests.Unit.Toolkit;

public static class Concurrency
{
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
