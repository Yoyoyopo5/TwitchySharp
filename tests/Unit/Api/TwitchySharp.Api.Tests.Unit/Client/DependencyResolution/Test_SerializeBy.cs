using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Tests.Unit.Toolkit;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution;

public class Test_SerializeByValue
{
    [Fact]
    public async Task ResolveOrDefault_MultipleWithSameValueKey_EvaluatedInSerial()
    {
        const int WORKER_COUNT = 64;
        Assert.True(WORKER_COUNT > 1);
        TimeSpan workerDelay = TimeSpan.FromMicroseconds(100);
        Concurrency.Probe probe = new();

        ResolveRequestDependency<string> stubResolver = async (scope, ct) =>
        {
            using IDisposable serializationBoundary = probe.Enter();
            await Task.Delay(workerDelay, ct);
            return string.Empty;
        };
        stubResolver = stubResolver.SerializeByValue<string, int>();

        int fixedKey = 0;
        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed<ImmutableRequestDependencyCollection, int?>(fixedKey);

        await Concurrency.RunConcurrently(WORKER_COUNT, async i =>
        {
            StubDependencyScope scope = new(dc);
            await stubResolver(scope, TestContext.Current.CancellationToken);
        }, TestContext.Current.CancellationToken);

        probe.AssertSerialExecution();
    }

    [Fact]
    public async Task ResolveOrDefault_MultipleWithUniqueKeys_EvaluatedInParallel()
    {
        const int WORKER_COUNT = 64;
        Assert.True(WORKER_COUNT > 1);
        TimeSpan workerDelay = TimeSpan.FromMilliseconds(100);

        Concurrency.Probe probe = new();

        ResolveRequestDependency<string> stubResolver = async (scope, ct) =>
        {
            using IDisposable serializationBoundary = probe.Enter();
            await Task.Delay(workerDelay, ct);
            return string.Empty;
        };
        stubResolver = stubResolver.SerializeByValue<string, int>();

        ImmutableRequestDependencyCollection dc = new();

        await Concurrency.RunConcurrently(WORKER_COUNT, async i =>
        {
            StubDependencyScope scope = new(dc.SetFixed<ImmutableRequestDependencyCollection, int?>(i));
            await stubResolver(scope, TestContext.Current.CancellationToken);
        }, TestContext.Current.CancellationToken);

        probe.AssertParallelExecution();
    }
}

public class Test_SerializeBy
{
    [Fact]
    public async Task ResolveOrDefault_MultipleWithSameReferenceKey_EvaluatedInSerial()
    {
        const int WORKER_COUNT = 64;
        Assert.True(WORKER_COUNT > 1);
        TimeSpan workerDelay = TimeSpan.FromMicroseconds(100);
        Concurrency.Probe probe = new();

        ResolveRequestDependency<int> stubResolver = async (scope, ct) =>
        {
            using IDisposable serializationBoundary = probe.Enter();
            await Task.Delay(workerDelay, ct);
            return 0;
        };
        stubResolver = stubResolver.SerializeBy<int, object>();

        object fixedKey = new();
        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed<ImmutableRequestDependencyCollection, object?>(fixedKey);

        await Concurrency.RunConcurrently(WORKER_COUNT, async i =>
        {
            StubDependencyScope scope = new(dc);
            await stubResolver(scope, TestContext.Current.CancellationToken);
        }, TestContext.Current.CancellationToken);

        probe.AssertSerialExecution();
    }

    [Fact]
    public async Task ResolveOrDefault_MultipleWithUniqueKeys_EvaluatedInParallel()
    {
        const int WORKER_COUNT = 64;
        Assert.True(WORKER_COUNT > 1);
        TimeSpan workerDelay = TimeSpan.FromMilliseconds(100);
        Concurrency.Probe probe = new();

        ResolveRequestDependency<string> stubResolver = async (scope, ct) =>
        {
            using IDisposable serializationBoundary = probe.Enter();
            await Task.Delay(workerDelay, ct);
            return string.Empty;
        };
        stubResolver = stubResolver.SerializeBy<string, object>();

        ImmutableRequestDependencyCollection dc = new();

        await Concurrency.RunConcurrently(WORKER_COUNT, async i =>
        {
            StubDependencyScope scope = new(dc.SetFixed<ImmutableRequestDependencyCollection, object?>(new()));
            await stubResolver(scope, TestContext.Current.CancellationToken);
        }, TestContext.Current.CancellationToken);

        probe.AssertParallelExecution();
    }
}
