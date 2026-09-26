using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.DependencyCollectionExtensions;

public class Test_ConfigureDispose
{
    [Fact]
    public async Task ResolveDependencyDisposalDictionary_ReturnsNotNull()
    {
        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .ConfigureDispose<ImmutableRequestDependencyCollection, object>(o => { });

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<DependencyDisposalDictionary>(TestContext.Current.CancellationToken)
            .MatchAsync(
                e => throw new Exception(e.Message),
                async dispose => Assert.NotNull(dispose));
    }

    [Fact]
    public async Task ResolveDependencyDisposalDictionaryThenInvoke_EvaluatesConfiguredDisposeFunction()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        bool disposed = false;

        object stubDependency = new();

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .ConfigureDispose<ImmutableRequestDependencyCollection, object>(o =>
            {
                Assert.Equal(stubDependency, o);
                disposed = true;
            });

        StubDependencyScope scope = new(dc);

        await scope.ResolveRequired<DependencyDisposalDictionary>(ct)
            .MapAsync(d => d.GetOrDefault(typeof(object))!.Invoke(stubDependency));

        Assert.True(disposed);
    }
}
