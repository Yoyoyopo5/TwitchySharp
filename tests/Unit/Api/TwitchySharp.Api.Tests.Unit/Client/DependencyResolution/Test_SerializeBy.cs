using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Tests.Unit.Toolkit;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution;

public class Test_SerializeByValue
{
    [Fact]
    public async Task ResolveOrDefault_MultipleWithSameValueKey_EvaluatedInSerial()
    {
        const int WORKER_COUNT = 256;
        int count = 0;

        ResolveRequestDependency<string> stubResolver = (scope, ct) =>
        {
            count++;
            return ValueTask.FromResult<Validation<string>>(string.Empty);
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

        Assert.Equal(WORKER_COUNT, count);
    }

    [Fact]
    public async Task ResolveOrDefault_MultipleWithUniqueKeys_EvaluatedInParallel()
    {
        const int WORKER_COUNT = 256;
        int count = 0;

        ResolveRequestDependency<string> stubResolver = (scope, ct) =>
        {
            count++;
            return ValueTask.FromResult<Validation<string>>(string.Empty);
        };
        stubResolver = stubResolver.SerializeByValue<string, int>();

        ImmutableRequestDependencyCollection dc = new();

        await Concurrency.RunConcurrently(WORKER_COUNT, async i =>
        {
            StubDependencyScope scope = new(dc.SetFixed<ImmutableRequestDependencyCollection, int?>(i));
            await stubResolver(scope, TestContext.Current.CancellationToken);
        }, TestContext.Current.CancellationToken);

        Assert.NotEqual(WORKER_COUNT, count);
    }
}

public class Test_SerializeBy
{
    [Fact]
    public async Task ResolveOrDefault_MultipleWithSameReferenceKey_EvaluatedInSerial()
    {
        const int WORKER_COUNT = 256;
        int count = 0;

        ResolveRequestDependency<int> stubResolver = (scope, ct) =>
        {
            count++;
            return ValueTask.FromResult<Validation<int>>(0);
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

        Assert.Equal(WORKER_COUNT, count);
    }

    [Fact]
    public async Task ResolveOrDefault_MultipleWithUniqueKeys_EvaluatedInParallel()
    {
        const int WORKER_COUNT = 256;
        int count = 0;

        ResolveRequestDependency<string> stubResolver = (scope, ct) =>
        {
            count++;
            return ValueTask.FromResult<Validation<string>>(string.Empty);
        };
        stubResolver = stubResolver.SerializeBy<string, object>();

        ImmutableRequestDependencyCollection dc = new();

        await Concurrency.RunConcurrently(WORKER_COUNT, async i =>
        {
            StubDependencyScope scope = new(dc.SetFixed<ImmutableRequestDependencyCollection, object?>(new()));
            await stubResolver(scope, TestContext.Current.CancellationToken);
        }, TestContext.Current.CancellationToken);

        Assert.NotEqual(WORKER_COUNT, count);
    }
}
