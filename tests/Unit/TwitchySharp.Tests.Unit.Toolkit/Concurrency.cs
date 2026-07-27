namespace TwitchySharp.Tests.Unit.Toolkit;

public static class Concurrency
{
    public static async Task<T[]> RunConcurrently<T>(
        int workerCount,
        ManualResetEventSlim gate,
        Func<int, Task<T>> func
    )
    {
        Task<T>[] tasks = Enumerable.Range(0, workerCount).Select(func).ToArray();
        gate.Set();
        return await Task.WhenAll(tasks);
    }

    public static async Task RunConcurrently(
        int workerCount,
        ManualResetEventSlim gate,
        Func<int, Task> func
        )
    {
        Task[] tasks = Enumerable.Range(0, workerCount).Select(func).ToArray();
        gate.Set();
        await Task.WhenAll(tasks);
    }
}
