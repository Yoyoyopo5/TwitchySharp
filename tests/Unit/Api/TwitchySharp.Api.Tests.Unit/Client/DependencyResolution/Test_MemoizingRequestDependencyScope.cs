using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution;

public class Test_MemoizingRequestDependencyScope
{
    private class StubDisposable : IDisposable
    {
        public bool Disposed { get; private set; } = false;
        public void Dispose() => Disposed = true;
    }

    [Fact]
    public void GetResolver_AfterSetResolver_ReturnsSetResolver()
    {
        ResolveRequestDependency<string> expected = (scope, ct)
            => ValueTask.FromResult<Validation<string>>("hello");

        ImmutableRequestDependencyCollection dc = new();

        MemoizingRequestDependencyScope scope = new(new StubTwitchRequest(), dc);
        scope.SetResolver(expected);

        ResolveRequestDependency<string>? actual = scope.GetResolver<string>();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetResolver_WithoutSetResolver_ReturnsNull()
    {
        ImmutableRequestDependencyCollection dc = new();

        MemoizingRequestDependencyScope scope = new(new StubTwitchRequest(), dc);

        ResolveRequestDependency<string>? actual = scope.GetResolver<string>();

        Assert.Null(actual);
    }

    [Fact]
    public async Task SetResolver_AfterResolvingDisposable_MemoizedValueDisposed()
    {
        StubDisposable disposable = new();

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(disposable);

        MemoizingRequestDependencyScope scope = new(new StubTwitchRequest(), dc);
        await scope.ResolveOrDefault<StubDisposable?>(TestContext.Current.CancellationToken);
        scope.SetResolver<ITwitchRequestDependencyCollection, StubDisposable>(scope => new StubDisposable());

        Assert.True(disposable.Disposed);
    }

    [Fact]
    public async Task ResolveOrDefault_WithResolverOnCollection_ReturnsResolverValue()
    {
        const string EXPECTED = "test_string";

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(EXPECTED);

        MemoizingRequestDependencyScope scope = new(new StubTwitchRequest(), dc);

        await scope.ResolveOrDefault<string>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(EXPECTED, v);
                return v;
            });
    }

    [Fact]
    public async Task ResolveOrDefault_DisposableResult_ThenDispose_DisposesMemoizedResult()
    {
        StubDisposable disposable = new();

        ITwitchRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(disposable);

        MemoizingRequestDependencyScope scope = new(new StubTwitchRequest(), dc);

        await scope.ResolveOrDefault<StubDisposable>(TestContext.Current.CancellationToken);
        scope.Dispose();

        Assert.True(disposable.Disposed);
    }

    [Fact]
    public async Task ResolveOrDefault_ThenSetResolver_ThenResolveOrDefault_EvaluatesNewResolver()
    {
        const string FIRST_VALUE = "first";
        const string SECOND_VALUE = "second";

        CancellationToken ct = TestContext.Current.CancellationToken;

        ITwitchRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(FIRST_VALUE);

        ITwitchRequestDependencyScope scope = new MemoizingRequestDependencyScope(new StubTwitchRequest(), dc);

        await scope.ResolveOrDefault<string>(ct);
        scope = scope.SetFixed(SECOND_VALUE);
        await scope.ResolveOrDefault<string>(ct).MapAsync(value => Assert.Equal(SECOND_VALUE, value));
    }

    [Fact]
    public async Task ResolveOrDefaultMultipleTimes_DoesNotEvaluateResolverAgainAndReturnsMemoizedValue()
    {
        const int RESOLVE_COUNT = 5;
        const string RESOLVER_VALUE = "something";
        int resolverCalledCount = 0;
        CancellationToken ct = TestContext.Current.CancellationToken;

        ITwitchRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetResolver(scope =>
            {
                resolverCalledCount++;
                return RESOLVER_VALUE;
            });

        ITwitchRequestDependencyScope scope = new MemoizingRequestDependencyScope(new StubTwitchRequest(), dc);

        string? result = null;
        foreach(int i in Enumerable.Range(0, RESOLVE_COUNT))
        {
            await scope.ResolveOrDefault<string>(ct).MapAsync(value => result = value);
        }

        Assert.Equal(RESOLVER_VALUE, result);
        Assert.Equal(1, resolverCalledCount);
    }

    [Fact]
    public async Task Dispose_WithDisposableMemoizedValues_DisposesMemoizedValues()
    {
        StubDisposable disposable = new();
        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(disposable);

        MemoizingRequestDependencyScope scope = new(new StubTwitchRequest(), dc);
        await scope.ResolveOrDefault<StubDisposable>(TestContext.Current.CancellationToken);

        scope.Dispose();

        Assert.True(disposable.Disposed);
    }
}
